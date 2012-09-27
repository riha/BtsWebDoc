using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class Orchestration : BizTalkBaseObject
    {
        public BizTalkAssembly ParentAssembly { get; set; }

        public Host Host { get; set; }

        public List<OrchestrationPort> Ports { get; set; }

        public override string Id
        {
            get { return Name; }
        }

        public Orchestration()
        {
            Ports = new List<OrchestrationPort>();
        }
    }
}