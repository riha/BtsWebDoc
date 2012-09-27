using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class Pipeline : BizTalkBaseObject
    {
        public string Type;

        public BizTalkAssembly ParentAssembly { get; set; }

        public override string Id
        {
            get { return Name; }
        }
    }
}