using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LittleTushy;
using LittleTushyClient;
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
            return $"Hello {name}";
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for(int i = 0; i < 1000; i++)
            {
                var response = await client.RequestAsync<string, string>("HelloWorld", "SayHello", "John Doe");
            }

            sw.Stop();
            var tushyElapsed = sw.Elapsed;

            sw.Reset();
            sw.Start();
            using (var httpClient = new HttpClient())
            {
                for(int i = 0; i < 1000; i++)
                {
                    var httpResult = await httpClient.PostAsync(
                        "http://localhost:5000/api/HelloWorldTest/SayHello",
                        new StringContent("John Doe", System.Text.Encoding.UTF8, "text/plain")
                    );
                }
            }
            var httpElapsed = sw.Elapsed;

            return "Tushy: " + tushyElapsed.TotalMilliseconds + " HTTP: " + httpElapsed.TotalMilliseconds;

        }


    }
}
