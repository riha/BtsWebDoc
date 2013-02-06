using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class Host : BizTalkBaseObject
    {
        public HostType Type { get; set; }
        public string NTGroupName { get; set; }

        public override string Id
        {
            get { return Name; }
        }

    }
}