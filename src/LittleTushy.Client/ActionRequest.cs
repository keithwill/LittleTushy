using ProtoBuf;
using System;

namespace LittleTushy.Client
{
    /// <summary>
    /// Action requests are used as a wrapper around all Little Tushy requests
    /// and have just enough information to locate the controller action to handle
    /// the request, the binary payload, and information about compression of the payload
    /// </summary>
    [ProtoContract]
    public class ActionRequest
    {

        [ProtoMember(1)]
        public string Controller;

        [ProtoMember(2)]
        public string Action;

        [ProtoMember(3)]
        public bool IsCompressed;

        [ProtoMember(4)]
        public byte[] Contents;
        
    }
}