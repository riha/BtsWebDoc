using System.Collections.Generic;
using btswebdoc.Model;

namespace btswebdoc.Web.Models
{
    public class SchemaViewModel : ApplicationViewModelBase
    {
        public Schema Schema { get; private set; }

        public SchemaViewModel(BizTalkApplication currentApplication, IEnumerable<Manifest> manifests, Manifest currentManifest, IEnumerable<BizTalkBaseObject> breadCrumbs, IEnumerable<BizTalkApplication> applications, IEnumerable<Host> hosts, Schema schema)
            : base(currentApplication, manifests, currentManifest, breadCrumbs, applications, hosts)
        {
            Schema = schema;
        }
    }
}