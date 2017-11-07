using ProtoBuf;

namespace LittleTushy 
{
    
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