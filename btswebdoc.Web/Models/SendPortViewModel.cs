using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class SendPortViewModel : ApplicationViewModelBase
    {
        public SendPort SendPort { get; set; }

        public SendPortViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, SendPort sendPort)
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            SendPort = sendPort;
        }
    }
}