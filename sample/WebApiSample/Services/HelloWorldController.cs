using System.Threading.Tasks;
using LittleTushy.Server;
using ProtoBuf;

namespace WebApiSample.Services
{
    public class HelloWorldController : ServiceController
    {

        [Action]
        public async Task<ActionResult> SayHello(string name)
        {
            //Fake a very small amount of work
            await Task.Delay(1);
            return Ok($"Hello {name}");
        }        


    }
}