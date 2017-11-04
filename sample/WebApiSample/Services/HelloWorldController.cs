using System.Threading.Tasks;
using LittleTushy;
using ProtoBuf;

namespace WebApiSample.Services
{
    public class HelloController : ServiceController
    {

        [Action]
        public async Task<ActionResult> SayHello(string name)
        {
            return Ok($"Hello {name}");
        }        


    }
}