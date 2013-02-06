using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class SendPortGroupViewModel : ApplicationViewModelBase
    {
        public SendPortGroup SendPortGroup { get; set; }

        public SendPortGroupViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, SendPortGroup sendPortGroup)
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            SendPortGroup = sendPortGroup;
        }
    }
}