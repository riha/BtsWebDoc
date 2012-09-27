using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class BizTalkInstallation
    {
        public IDictionary<string, BizTalkApplication> Applications { get; set; }

        public BizTalkInstallation()
        {
            Applications = new Dictionary<string, BizTalkApplication>();
        }

        //public IList<Pipeline> Pipelines { get; set; }

        //public IList<Orchestration> Orchestrations { get; set; }

        //public IList<Schema> Schemas { get; set; }

        //public IList<BizTalkAssembly> Assemblies { get; set; }

        //public IList<ReceivePort> ReceivePorts { get; set; }

        //public IList<SendPort> SendPorts { get; set; }

        //public IList<Transform> Transforms { get; set; }

        //public string DatabaseServer { get; set; }

        //public string DatabaseName { get; set; }
    }
}