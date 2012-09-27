using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using btswebdoc.Model;

namespace btswebdoc.CmdClient
{
    public class BizTalkArtifacts
    {
        public IDictionary<string, BizTalkApplication> Applications { get; set; }
        public IDictionary<string, Pipeline> Pipelines { get; set; }
        public IDictionary<string, Orchestration> Orchestrations { get; set; }
        public IDictionary<string, Schema> Schemas { get; set; }
        public IDictionary<string, BizTalkAssembly> Assemblies { get; set; }
        public IDictionary<string, ReceivePort> ReceivePorts { get; set; }
        public IDictionary<string, SendPort> SendPorts { get; set; }
        public IDictionary<string, Transform> Transforms { get; set; }
    }
}
