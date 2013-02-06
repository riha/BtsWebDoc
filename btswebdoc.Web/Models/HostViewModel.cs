using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class HostViewModel : HostViewModelBase
    {
        public Host Host { get; set; }

        public HostViewModel(Host currentHost, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, Host host)
            : base(currentHost, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            Host = host;
        }
    }
}