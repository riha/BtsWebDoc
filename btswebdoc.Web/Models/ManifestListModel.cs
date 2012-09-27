using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class ManifestListModel
    {
        public BizTalkBaseObject Artefact { get; set; }
        public IEnumerable<Manifest> Manifests { get; set; }
        public Manifest CurrentManifest { get; set; }
    }
}