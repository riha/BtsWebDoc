using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class HomeViewModel:ViewModelBase
    {
        public HomeViewModel(IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts) 
            : base(manifests, currentManifest, breadCrumbs, applications, hosts)
        {
        }
    }
}