﻿using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LittleTushy.Server
{
    public static class LittleTushyExtensionMethods
    {
        private static readonly Type ServiceControllerType = typeof(ServiceController);
        private static readonly Type ActionResultType = typeof(ActionResult);

        private static readonly Type TaskType = typeof(Task);

        /// <summary>
        /// Configures Little Tushy and adds it to the services collection.
        /// This will reflect over types from the calling project, and register
        /// Classes that inherit from ServiceController and put them and their methods
        /// into the service map.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="littleTushyOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddLittleTushy(
            this IServiceCollection serviceCollection,
            LittleTushyOptions littleTushyOptions = null
            )
        {
            if (littleTushyOptions == null)
            {
                littleTushyOptions = new LittleTushyOptions();
            }
            serviceCollection.AddSingleton(littleTushyOptions);
            serviceCollection.AddSingleton<LittleTushyServer>();

            //Getting the calling assembly relies on who is calling the current method
            //If you move this line when refactoring into a deeper method...it will break
            var assembly = Assembly.GetCallingAssembly();
            MapControllers(serviceCollection, assembly);

            return serviceCollection;
        }

        /// <summary>
        /// Add the Little Tushy middleware to the application to start listening for web socket requests
        /// </summary>
        public static IApplicationBuilder UseLittleTushy(this IApplicationBuilder app)
        {

            var server = app.ApplicationServices.GetService<LittleTushyServer>();
            var littleTushyOptions = app.ApplicationServices.GetService<LittleTushyOptions>();

            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == littleTushyOptions.WebSocketRequestPath)
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await server.HandleClientAsync(webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });
            return app;
        }

        /// <summary>
        /// Find the the ServiceControllers and register them and add their methods to the service map
        /// </summary>
        private static void MapControllers(IServiceCollection serviceCollection, Assembly assembly)
        {
            var servicesTemp = new Dictionary<string, ServiceControllerDefinition>();
            var assemblyTypes = assembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                MapType(serviceCollection, servicesTemp, type);
            }

            var serviceMap = new ServiceMap(servicesTemp);

            serviceCollection.AddSingleton(serviceMap);
        }


        /// <summary>
        /// Map each service controller's Action annotated methods to the service map
        /// </summary>
        private static void MapType(IServiceCollection serviceCollection, Dictionary<string, ServiceControllerDefinition> servicesTemp, Type type)
        {
            if (type.BaseType == ServiceControllerType)
            {

                //Instantiate a definition of the controller to add to the service map
                var serviceController = new ServiceControllerDefinition(type);
                servicesTemp.Add(serviceController.Name, serviceController);

                //Register the controller so that it can later be resolved by IoC
                serviceCollection.AddSingleton(type);

                var actions = new List<ServiceActionDefinition>();
                var methods = type.GetMethods();

                foreach (var method in methods)
                {
                    if (Attribute.GetCustomAttribute(method, typeof(ActionAttribute)) is ActionAttribute operationAttribute)
                    {
                        
                        var returnType = method.ReturnType;

                        if (returnType.BaseType != TaskType)
                        {
                            if (ActionResultType.IsAssignableFrom(returnType))
                            {
                                actions.Add(new ServiceActionDefinition(
                                    serviceController.Name,
                                     method.Name,
                                      method,
                                       returnType,
                                        false, 
                                        operationAttribute.Compress));
                                continue;
                            }
                            else
                            {
                                throw new InvalidOperationException($"Action {method.Name} on controller {serviceController.Name} must have a return type of ActionResult or Task<ActionResult>");
                            }
                        }

                        var taskTResultTypes = returnType.GetGenericArguments();

                        if (taskTResultTypes.Length == 1)
                        {
                            var taskResultType = taskTResultTypes[0];

                            if (ActionResultType.IsAssignableFrom(taskResultType))
                            {
                                actions.Add(new ServiceActionDefinition(
                                    serviceController.Name,
                                     method.Name,
                                      method,
                                       returnType,
                                        true,
                                        operationAttribute.Compress));
                                continue;
                            }
                            else
                            {
                                throw new InvalidOperationException($"Action {method.Name} on controller {serviceController.Name} must have a return type of ActionResult or Task<ActionResult>");
                            }

                        }

                    }
                }

                serviceController.SetActions(actions);


            }
        }



    }
}
