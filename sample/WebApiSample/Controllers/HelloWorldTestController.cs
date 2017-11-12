using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LittleTushy.Server;
using LittleTushy.Client;
using System.Diagnostics;
using System.Net.Http;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class HelloWorldTestController : Controller
    {
        private readonly ServiceClient client;

        public HelloWorldTestController(ServiceClient client)
        {
            this.client = client;
        }

        

        [HttpGet]
        [Route("/SayHello/{name}")]
        public async Task<string> SayHello(string name)
        {
            return $"Hello {name}";
        }

        /// <summary>
        /// This is a web api operation that uses the Little Tushy client to connect to localhost
        /// and call a controller action (hosted in this same web application using Little Tushy
        /// server) that just gets back a hello string that includes the name passed to the
        /// Service Controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {

            return (

                await client.RequestAsync<string, string>
                (
                    "HelloWorld", //controller
                    "SayHello", //action
                    "John Doe" //parameter
                )
                
            ).Result;

        }


    }
}
