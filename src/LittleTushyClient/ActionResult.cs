using ProtoBuf;

namespace LittleTushyClient
{
    [ProtoContract]
    public class ActionResult<TResult>
    {

        [ProtoMember(1)]
        public StatusCode StatusCode;

        [ProtoMember(2)]
        public string Message;

        [ProtoMember(3)]
        public byte[] Contents;

        public TResult Result { get; set; }

    }
}