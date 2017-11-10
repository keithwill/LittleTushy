using ProtoBuf;

namespace LittleTushy.Server
{
    
    /// <summary>
    /// A base class for all Service Controller instances that handle Little Tushy requests.
    /// This can be used like Web API controllers and contains convencience methods for formatting
    /// results
    /// </summary>
    public abstract class ServiceController {

        public ActionResult Ok(object contents)
        {
            return new ActionResult{
                StatusCode = StatusCode.Ok,
                Contents = GetBinaryContents(contents)
            };
        }

        public byte[] GetBinaryContents(object contents)
        {
            using (var mem = new System.IO.MemoryStream())
            {
                Serializer.Serialize(mem, contents);
                return mem.ToArray();
            }
        }

     }

}