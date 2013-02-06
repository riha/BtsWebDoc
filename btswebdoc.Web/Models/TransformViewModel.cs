using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class TransformViewModel : ApplicationViewModelBase
    {
        public Transform Transform { get; set; }

        public TransformViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, Transform transform) 
            : base(currentApplication, manifests, currentManifest, breadCrumbs,  applications, hosts)
        {
            Transform = transform;
        }
    }
}