using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class Host : BizTalkBaseObject
    {
        public override string Id
        {
            get { return Name; }
        }

    }
}