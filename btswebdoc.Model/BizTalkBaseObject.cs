using System;

namespace btswebdoc.Model
{
    [Serializable]
    public abstract class BizTalkBaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public BizTalkApplication Application { get; set; }

        public abstract string Id { get; }
    }
}