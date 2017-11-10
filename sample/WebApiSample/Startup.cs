using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LittleTushy.Server;
using LittleTushy.Client;

namespace WebApiSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /// <summary>
            /// You normally wouldn't need to register the ServiceClient on the server
            /// side of your application, but in this sample app we both host and call
            /// a Little Tushy service
            /// </summary>
            services.AddSingleton(new ServiceClient("localhost", 5000, false));

            services.AddMvc();
            
            //This line searches for Service Controllers, registers them in IoC,
            //and adds them to the service map, and prepares cached invocation
            //calls for the controller action methods
            services.AddLittleTushy(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Running side by side with Mvc / Web API is a primary use case of Little Tushy
            app.UseMvc();
            //This line adds that Little Tushy middleware that listens for WebSocket
            //requests on the /lt path and converts them into WebSocket connections
            //then enters a loop to handle requests and return responses to the WebSocket
            //connection, using the service map to locate the appropriate controllers to
            //invoke
            app.UseLittleTushy();
        }
    }
}
