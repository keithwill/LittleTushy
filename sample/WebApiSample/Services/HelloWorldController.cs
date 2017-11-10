using System.Threading.Tasks;
using LittleTushy.Server;
using LittleTushy;
using ProtoBuf;

namespace WebApiSample.Services
{
    /// <summary>
    /// A simple Service Controller that returns a string saying
    /// hello to the name passed as a parameter.
    /// 
    /// Notice that the SayHello method is annotated with the 
    /// Action attribute and has Compression enabled
    /// </summary>
    public class HelloWorldController : ServiceController
    {


        /// <summary>
        /// A sample controller action that says hello to the name passed in
        /// Controller actions must return ActionResult, or Task&lt;ActionResult&gt;
        /// (depending on if the method is synchronous or async)
        /// </summary>
        /// <param name="name">The name to say hello to</param>
        /// <remarks>Compression is only used here to show the usage. It increases
        /// the size of the result for payloads this small</remarks>
        [Action(Compress = true)]
        public async Task<ActionResult> SayHello(string name)
        {

            //Fake a very small amount of work
            await Task.Delay(1);

            //Convenience methods for serializing the result contents
            //and setting the status code are the preferred way to return
            //from a service method, but there is no magic in the convenience methods
            // and instantiating an ActionResult directly is perfectly fine as well.
            return Ok($"Hello {name}");

            //Equivilent code to serialize a string using protobuf and
            //return it in an Ok result

            // using (var mem = new System.IO.MemoryStream())
            // {
            //     Serializer.Serialize(mem, $"Hello {name}");
            //     var contents = mem.ToArray();
            //     return new ActionResult
            //     {
            //         StatusCode = StatusCode.Ok,
            //         Contents = contents
            //     };
            // }

        }        


    }
}