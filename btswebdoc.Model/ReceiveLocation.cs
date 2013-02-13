using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class ReceiveLocation : BizTalkBaseObject
    {
        public string Address { get; set; }

        public string TransportProtocol { get; set; }

        public Pipeline ReceivePipeline { get; set; }

        public Host Host { get; set; }

        public Pipeline SendPipeline { get; set; }

        public List<Orchestration> Orchestrations { get; set; }

        public ReceivePort ReceivePort { get; set; }

        public override string Id
        {
            get { return Name; }
        }

        public ReceiveLocation()
        {
            Orchestrations = new List<Orchestration>();
        }
    }
}