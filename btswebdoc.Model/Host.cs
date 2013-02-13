using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class Host : BizTalkBaseObject
    {
        public HostType Type { get; set; }
        public string NTGroupName { get; set; }
        public List<Orchestration> Orchestrations { get; set; }
        public List<SendPort> SendPorts { get; set; }
        public List<ReceiveLocation> ReceiveLocations { get; set; }

        public override string Id
        {
            get { return Name; }
        }

        public Host()
        {
            Orchestrations = new List<Orchestration>();
            SendPorts = new List<SendPort>();
            ReceiveLocations = new List<ReceiveLocation>();
        }

    }
}