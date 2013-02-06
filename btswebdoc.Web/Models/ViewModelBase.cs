using System.Collections.Generic;
using System.Linq;
using btswebdoc.Model;
using btswebdoc.Web.Extensions;

namespace btswebdoc.Web.Models
{
    public abstract class ViewModelBase
    {
        public IEnumerable<BizTalkBaseObject> BreadCrumbs { get; set; }
        public IEnumerable<Manifest> Manifests { get; set; }
        //public BizTalkBaseObject CurrentArtefact { get; set; }
        public IEnumerable<BizTalkApplication> Applications { get; set; }
        public IEnumerable<Host> Hosts { get; set; }
        public Manifest CurrentManifest { get; set; }

        protected ViewModelBase(IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts)
        {
            Manifests = manifests;
            //CurrentArtefact = currentArtefact;
            CurrentManifest = currentManifest;
            BreadCrumbs = breadCrumbs;
            Applications = applications;
            Hosts = hosts;
        }
    }
}