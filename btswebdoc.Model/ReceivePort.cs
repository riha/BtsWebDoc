using System;
using System.Collections.Generic;
using System.Web;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
        [Serializable]
    public class ReceivePort : BizTalkBaseObject
    {
        public readonly List<Orchestration> BoundOrchestrations;

        public string AuthenticationType { get; set; }

        public int TrackingType { get; set; }

        public bool TwoWay { get; set; }

        public List<ReceiveLocation> ReceiveLocations { get; set; }

        public List<Transform> InboundTransforms { get; set; }

        public List<Transform> OutboundTransforms { get; set; }

        public override string Id
        {
            get { return Name.GetUrlSafe(); }
        }

        public ReceivePort()
        {
            ReceiveLocations = new List<ReceiveLocation>();
            InboundTransforms = new List<Transform>();
            OutboundTransforms = new List<Transform>();
            BoundOrchestrations = new List<Orchestration>();
        }
    }
}