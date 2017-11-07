using ProtoBuf;

namespace LittleTushy.Server
{
    [ProtoContract]
    public class ActionResult
    {

        [ProtoMember(1)]
        public StatusCode StatusCode;

        [ProtoMember(2)]
        public string Message;

        [ProtoMember(3)]
        public byte[] Contents;

    }
}