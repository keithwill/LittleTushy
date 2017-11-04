using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LittleTushy;
using LittleTushyClient;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class HelloWorldTestController : Controller
    {

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            using (var client = new ServiceClient(new Uri("ws://localhost:5000/lt")))
            {
                return await client.RequestAsync<string, string>("HelloWorld", "SayHello", "John Doe");
            }
            
        }

 
    }
}
