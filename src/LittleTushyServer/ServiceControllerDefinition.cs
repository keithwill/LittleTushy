using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleTushy
{
    /// <summary>
    /// Details about a controller that was detected by LittleTushy on startup 
    /// that will be found in the ServiceMap.
    /// </summary>
    public class ServiceControllerDefinition
    {

        private readonly Type Type;
        /// <summary>
        /// The name of the controller
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// A lookup by action name of the action methods defined on the controller
        /// </summary>
        public IReadOnlyDictionary<string, ServiceActionDefinition> Actions;

        /// <summary>
        /// Instantiates a controller definition. This is normallly called by the
        /// LittleTushy extension method at startup, but may be useful for manually
        /// testing LittleTushy routing
        /// </summary>
        /// <param name="controllerType"></param>
        public ServiceControllerDefinition(Type controllerType)
        {
            this.Type = controllerType;

            if (controllerType.Name.EndsWith("Controller"))
            {
                Name = controllerType.Name.Replace("Controller", "");
            }
            else
            {
                Name = controllerType.Name;
            }
        }

        internal void SetActions(List<ServiceActionDefinition> serviceActions)
        {
            Actions = serviceActions.ToDictionary(x => x.Action, x => x);
        }

        
    }
}
