using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class OrchestrationViewModel : ApplicationViewModelBase
    {
        public Orchestration Orchestration { get; set; }

        public OrchestrationViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, Orchestration orchestration) 
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            Orchestration = orchestration;
        }
    }
}