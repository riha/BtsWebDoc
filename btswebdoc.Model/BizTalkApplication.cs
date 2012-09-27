using System;
using System.Collections.Generic;
using System.Web;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
    [Serializable]
    public class BizTalkApplication : BizTalkBaseObject
    {
        public List<BizTalkApplication> ReferencedApplications { get; set; }

        public IDictionary<string, BizTalkAssembly> Assemblies { get; set; }

        public IDictionary<string, ReceivePort> ReceivePorts { get; set; }

        public IDictionary<string, SendPort> SendPorts { get; set; }

        public IDictionary<string, Orchestration> Orchestrations { get; set; }

        public IDictionary<string, Schema> Schemas { get; set; }

        public IDictionary<string, Transform> Maps { get; set; }

        public IDictionary<string, Pipeline> Pipelines { get; set; }

        public override string Id
        {
            get { return Name.GetUrlSafe(); }
        }

        public BizTalkApplication()
        {
            ReferencedApplications = new List<BizTalkApplication>();
            ReceivePorts = new Dictionary<string, ReceivePort>();
            SendPorts = new Dictionary<string, SendPort>();
            Orchestrations = new Dictionary<string, Orchestration>();
            Schemas = new Dictionary<string, Schema>();
            Assemblies = new Dictionary<string, BizTalkAssembly>();
            Maps = new Dictionary<string, Transform>();
            Pipelines = new Dictionary<string, Pipeline>();

        }
    }
}