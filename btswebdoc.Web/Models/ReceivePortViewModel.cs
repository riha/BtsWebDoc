using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class ReceivePortViewModel : ApplicationViewModelBase
    {
        public ReceivePort ReceivePort { get; set; }

        public ReceivePortViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, ReceivePort receivePort) 
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            ReceivePort = receivePort;
        }
    }
}