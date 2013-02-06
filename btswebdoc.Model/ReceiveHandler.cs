using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class ReceiveHandler : BizTalkBaseObject
    {
        public Host Host { get; set; }
        public override string Id
        {
            get { return Name; }
        }
    }
}