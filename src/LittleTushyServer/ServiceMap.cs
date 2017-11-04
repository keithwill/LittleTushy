using System;
using System.Collections.Generic;
using System.Text;

namespace LittleTushy
{

    /// <summary>
    /// Holds references to controllers and action methods found on startup, and 
    /// can retrieve a controller and action definition from a service name
    /// (a string of the pattern 'controller/action')
    /// </summary>
    public class ServiceMap
    {
        /// <summary>
        /// The readonly lookup of controller definitions by name
        /// </summary>
        public readonly IReadOnlyDictionary<string, ServiceControllerDefinition> Controllers;

        /// <summary>
        /// Instanciates a ServiceMap. This is normally instantiated by the 
        /// extension method to add LittleTushy to a ServiceCollection, but can be instantiated
        /// directly if desired for testing.
        /// </summary>
        /// <param name="controllers"></param>
        public ServiceMap(Dictionary<string, ServiceControllerDefinition> controllers)
        {
            Controllers = controllers;
        }

        /// <summary>
        /// Retrieves an action definition for a controller based on a
        /// serviceName (a string of the pattern 'controller/action')
        /// </summary>
        /// <param name="serviceName">A string of the pattern 'controller/action'</param>
        /// <returns>The ServiceActionDefinition that matches the serviceName, or null if one is not found</returns>
        public ServiceActionDefinition GetActionDefintion(string controllerName, string actionName)
        {
            Controllers.TryGetValue(controllerName, out var controller);
            if (controller == null){return null;}
            controller.Actions.TryGetValue(actionName, out var action);
            return action;
        }

    }
}
