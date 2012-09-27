using System;
using System.Collections.Generic;
using System.Web;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
        [Serializable]
    public class SendPort : BizTalkBaseObject
    {
        public readonly List<Orchestration> BoundOrchestrations;

        public List<FilterGroup> FilterGroups { get; set; }

        public int Priority { get; set; }

        public string TrackingType { get; set; }

        public bool RouteFailedMessage { get; set; }

        public bool StopSendingOnFailure { get; set; }

        public bool Dynamic { get; set; }

        public bool TwoWay { get; set; }

        public Pipeline SendPipeline { get; set; }

        public TransportInfo PrimaryTransport { get; set; }

        public TransportInfo SecondaryTransport { get; set; }

        public List<Transform> OutboundTransforms { get; set; }

        public List<Transform> InboundTransforms { get; set; }

        public override string Id
        {
            get { return Name.GetUrlSafe(); }
        }

        public SendPort()
        {
            FilterGroups = new List<FilterGroup>();
            InboundTransforms = new List<Transform>();
            OutboundTransforms = new List<Transform>();
            BoundOrchestrations = new List<Orchestration>();
        }
    }
}