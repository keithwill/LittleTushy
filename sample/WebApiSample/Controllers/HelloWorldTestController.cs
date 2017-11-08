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

        [HttpPost]
        public async Task<string> SayHello(string name)
        {
            //Fake a very small amount of work
            await Task.Delay(1);
            return $"Hello {name}";
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {

            return (

                await client.RequestAsync<string, string>(
                    "HelloWorld", 
                    "SayHello",
                    "John Doe"
                )
                
            ).Result;

        }


    }
}
