using System;
using System.Collections.Generic;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
    [Serializable]
    public class BizTalkAssembly : BizTalkBaseObject
    {
        public string QualifiedName { get; set; }

        public List<Schema> Schemas { get; set; }

        public List<Transform> Transforms { get; set; }

        public List<Orchestration> Orchestrations { get; set; }

        public List<Pipeline> Pipelines { get; set; }

        public string Version { get; set; }

        public string PublicKeyToken { get; set; }

        public string Culture { get; set; }

        public override string Id
        {
            get { return QualifiedName.GetCheckSum(); }
        }

        public BizTalkAssembly()
        {
            Pipelines = new List<Pipeline>();
            Schemas = new List<Schema>();
            Transforms = new List<Transform>();
            Orchestrations = new List<Orchestration>();
        }
    }
}