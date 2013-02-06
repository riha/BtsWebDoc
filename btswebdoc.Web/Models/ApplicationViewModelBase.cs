using System.Collections.Generic;
using System.Linq;
using btswebdoc.Model;
using btswebdoc.Web.Extensions;

namespace btswebdoc.Web.Models
{
    public abstract class ApplicationViewModelBase : ViewModelBase
    {
        public BizTalkApplication CurrentApplication { get; private set; }

        protected ApplicationViewModelBase(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts)
            : base(manifests, currentManifest,breadCrumbs, applications,hosts)
        {
            CurrentApplication = currentApplication;
        }
    }
}