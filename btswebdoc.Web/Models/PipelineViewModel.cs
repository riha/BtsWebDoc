using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class PipelineViewModel : ApplicationViewModelBase
    {
        public Pipeline Pipeline { get; set; }

        public PipelineViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, Pipeline pipeline)
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            Pipeline = pipeline;
        }
    }
}