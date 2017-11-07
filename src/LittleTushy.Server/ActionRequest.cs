using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleTushy.Server
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