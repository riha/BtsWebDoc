using System.Collections.Generic;
using System.Linq;
using btswebdoc.Model;
using btswebdoc.Web.Extensions;

namespace btswebdoc.Web.Models
{
    public abstract class HostViewModelBase : ViewModelBase
    {
        public Host CurrentHost { get; private set; }

        protected HostViewModelBase(Host currentHost, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts)
            : base(manifests, currentManifest,breadCrumbs, applications,hosts)
        {
            CurrentHost = currentHost;
        }
    }
}