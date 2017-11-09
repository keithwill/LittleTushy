using Fasterflect;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LittleTushy.Server.LZ4;

namespace LittleTushy.Server
{
    /// <summary>
    /// The definition of a controller action that was detected on startup by LittleTushy
    /// and contains necessary references to dynamically invoke the controller action method
    /// at runtime with serialized parameters provided by the LittleTushyRequestHandler.
    /// </summary>
    public class ServiceActionDefinition
    {

        /// <summary>
        /// The name of the controller
        /// </summary>
        public readonly string Controller;

        /// <summary>
        /// The name of the action method on the controller
        /// </summary>
        public readonly string Action;

        private readonly MethodInfo methodInfo;

        /// <summary>
        /// The payload generic type of the Task of ServiceResponse of the action method 
        /// </summary>
        public readonly Type ReturnType;
        private readonly bool isAsync;
        public readonly bool Compress;
        private readonly MethodInvoker invoker;

        /// <summary>
        /// The System.Type of the controller
        /// </summary>
        public readonly Type ControllerType;

        /// <summary>
        /// Whether or not the action method of the controller has a method parameter
        /// </summary>
        public readonly bool HasParameter;

        /// <summary>
        /// The System.Type of the parameter (if any) to the action method of the controller
        /// </summary>
        public readonly Type ParameterType;

        public readonly string ParameterName;

        /// <summary>
        /// Instantiates a controller action definition. This is typically 
        /// instantiated by the LittleTushy extension method on startup, but could be
        /// useful to instantiate directly to test controller serviceName routing.
        /// </summary>
        /// <param name="controller">The name of the controller this action method definition was found on</param>
        /// <param name="action">The name of the method on the controller</param>
        /// <param name="methodInfo">The reflection methodInfo for the method</param>
        /// <param name="returnType">The 'payload' Type of the method (ignoring the Task and ServiceResponse generic wrapping)</param>
        public ServiceActionDefinition(
            string controller, 
            string action, 
            MethodInfo methodInfo, 
            Type returnType, 
            bool isAsync,
            bool compress
            )
        {
            this.Controller = controller;
            this.Action = action;
            this.methodInfo = methodInfo;
            this.ReturnType = returnType;
            this.isAsync = isAsync;
            this.Compress = compress;
            this.ControllerType = methodInfo.DeclaringType;
            invoker = methodInfo.DelegateForCallMethod();
            var parameters = methodInfo.GetParameters();
            HasParameter = methodInfo.GetParameters().Length == 1;
            if (HasParameter)
            {
                ParameterType = parameters[0].ParameterType;
                ParameterName = parameters[0].Name;
            }
        }

        /// <summary>
        /// Deserializes the service action parameter into the expected type and 
        /// dynamically invokes the action method on the passed in controller instance
        /// and returns the result of the action.
        /// </summary>
        /// <param name="controller">The controller instance to invoke the action on</param>
        /// <param name="parameter">The serialized bytes of the parameter to the controller action to invoke</param>
        /// <returns>Returns an async task to await the untyped result of the controller action that was invoked.
        /// This is expected to be a generic variant of ServiceResponse where the generic
        /// parameter is serializable by protobuf.</returns>
        public async Task<ActionResult> InvokeFunction(ServiceController controller, ActionRequest request)
        {

            byte[] parameter = request.Contents;
            if (HasParameter)
            {
                object parameterInstance;
                try
                {
                    
                    using (var mem = new MemoryStream(parameter))
                    {
                        if (request.IsCompressed)
                        {
                            using (var lz4stream = new LZ4Stream(mem, LZ4StreamMode.Decompress))
                            {
                                parameterInstance = Serializer.Deserialize(ParameterType, lz4stream);
                            }
                        }
                        else
                        {
                            parameterInstance = Serializer.Deserialize(ParameterType, mem);
                        }
                    }
                }
                catch (ProtoException ex)
                {
                    return new ActionResult
                    {
                        Message = $"Could not understand the parameter {ParameterName} when deserializing - " + ex.ToString(),
                        StatusCode = StatusCode.BadRequest
                    };
                }

                if (isAsync)
                {
                    return await (Task<ActionResult>)invoker(controller, parameterInstance);
                }
                else
                {
                    return (ActionResult)invoker(controller, parameterInstance);
                }


            }

            if (isAsync)
            {
                return await (Task<ActionResult>)invoker(controller, new object[] { });

            }
            else
            {
                return (ActionResult)invoker(controller, new object[] { });
            }


        }



    }
}
