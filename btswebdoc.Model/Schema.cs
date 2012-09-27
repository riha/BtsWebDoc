using System;
using System.Collections.Generic;
using btswebdoc.Shared.Extensions;

namespace btswebdoc.Model
{
    [Serializable]
    public class Schema : BizTalkBaseObject
    {
        public List<KeyValuePair<string, string>> Properties;

        public List<Schema> ReferencedSchemas;

        public string RootName { get; set; }

        public string QualifiedName { get; set; }

        public string TargetNamespace { get; set; }

        public SchemaType SchemaType { get; set; }

        public BizTalkAssembly ParentAssembly { get; set; }

        public override string Id
        {
            get { return string.Concat(Name, RootName, TargetNamespace).GetCheckSum(); }
        }

        public Schema()
        {
            Properties = new List<KeyValuePair<string, string>>();
            ReferencedSchemas = new List<Schema>();
        }

    }
}