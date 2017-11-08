using ProtoBuf;
using System;

namespace LittleTushy.Client
{
    [ProtoContract]
    public class ActionRequest
    {

        [ProtoMember(1)]
        public string Controller;

        [ProtoMember(2)]
        public string Action;

        [ProtoMember(3)]
        public byte[] Contents;
        
    }
}