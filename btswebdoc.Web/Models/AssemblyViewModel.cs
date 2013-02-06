using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class AssemblyViewModel : ApplicationViewModelBase
    {
        public BizTalkAssembly Assembly { get; set; }

        public AssemblyViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, BizTalkAssembly assembly) 
            : base(currentApplication, manifests, currentManifest, breadCrumbs,  applications, hosts)
        {
            Assembly = assembly;
        }
    }
}