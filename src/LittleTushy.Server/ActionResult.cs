using ProtoBuf;

namespace LittleTushy.Server
{
    /// <summary>
    /// Action results are used as a wrapper around all responses from controller action methods
    /// </summary>
    [ProtoContract]
    public class ActionResult
    {

        [ProtoMember(1)]
        public StatusCode StatusCode;

        [ProtoMember(2)]
        public string Message;

        [ProtoMember(3)]
        public bool IsCompressed {get;set;}

        [ProtoMember(4)]
        public byte[] Contents;

    }
}