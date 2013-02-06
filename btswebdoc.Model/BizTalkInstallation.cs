using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class BizTalkInstallation
    {
        public IDictionary<string, BizTalkApplication> Applications { get; set; }
        public IDictionary<string, Host> Hosts { get; set; }

        public BizTalkInstallation()
        {
            Applications = new Dictionary<string, BizTalkApplication>();
            Hosts = new Dictionary<string, Host>(); 
        }
    }
}