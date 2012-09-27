using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class ReceiveHandler : BizTalkBaseObject
    {
        public override string Id
        {
            get { return Name; }
        }
    }
}