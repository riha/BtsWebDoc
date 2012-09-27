using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class Transform : BizTalkBaseObject
    {
        public List<ReceivePort> ReceivePorts { get; set; }

        public List<SendPort> SendPorts { get; set; }

        public BizTalkAssembly ParentAssembly { get; set; }

        public Schema SourceSchema { get; set; }

        public Schema TargetSchema { get; set; }

        public override string Id
        {
            get { return Name; }
        }

        public Transform()
        {
            ReceivePorts = new List<ReceivePort>();
            SendPorts = new List<SendPort>();
        }
    }
}