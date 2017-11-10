using ProtoBuf;
using LittleTushy;

namespace LittleTushy.Client
{
    /// <summary>
    /// All Little Tushy server responses are wrapped in an ActionResult
    /// to give access to Http style status codes, the payload, and an 
    /// optional message. On the client side the ActionResult class is
    /// typed to the result type of the RequestAsync method called
    /// to create it 
    /// </summary>
    [ProtoContract]
    public class ActionResult<TResult>
    {
        [ProtoMember(1)]
        public StatusCode StatusCode;

        [ProtoMember(2)]
        public string Message;

        [ProtoMember(3)]
        public bool IsCompressed {get;set;}

        [ProtoMember(4)]
        public byte[] Contents;
        
        public TResult Result { get; set; }
    }
}