using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class ApplicationViewModel : ApplicationViewModelBase
    {
        public BizTalkApplication Application { get; set; }

        public ApplicationViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, BizTalkApplication application)
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            Application = application;
        }
    }
}