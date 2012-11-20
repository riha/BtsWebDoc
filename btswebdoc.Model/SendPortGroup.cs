using System;
using System.Collections.Generic;
using System.Web;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
    [Serializable]
    public class SendPortGroup : BizTalkBaseObject
    {
        public readonly List<Orchestration> BoundOrchestrations;

        public List<FilterGroup> FilterGroups { get; set; }

        public List<SendPort> SendPorts { get; set; }

        public override string Id
        {
            get { return Name.GetUrlSafe(); }
        }

        public SendPortGroup()
        {
            FilterGroups = new List<FilterGroup>();
            SendPorts = new List<SendPort>();
        }
    }
}