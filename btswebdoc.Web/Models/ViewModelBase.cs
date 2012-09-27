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
        public BizTalkApplication CurrentApplication { get; set; }
        public IEnumerable<BizTalkApplication> Applications { get; set; }
        //public BizTalkInstallation Installation { private get; set; }
        public Manifest CurrentManifest { get; set; }

        //public Orchestration GetOrchestration(string name)
        //{
        //    return Installation.GetOrchestration(name);
        //}

        //public BizTalkAssembly GetAssembly(string id)
        //{
        //    return Installation.GetAssembly(id);
        //}

        //public Host GetHost(string name)
        //{
        //    return Installation.GetHost(name);
        //}

        //public Transform GetTransform(string name)
        //{
        //    return Installation.GetTransform(name);
        //}

        //public BizTalkApplication GetApplication(string name)
        //{
        //    return Installation.GetApplication(name);
        //}


        //public ReceivePort GetReceivePort(string name)
        //{
        //    return Installation.GetReceivePort(name);
        //}

        //public SendPort GetSendPort(string name)
        //{
        //    return Installation.GetSendPort(name);
        //}

        //public Schema GetSchema(string id)
        //{
        //    return Installation.GetSchema(id);
        //}

        //public Pipeline GetPipeline(string name)
        //{
        //    return Installation.GetPipeline(name);
        //}

        //public IEnumerable<BizTalkApplication> Applications
        //{
        //    get { return Installation.Applications.Values; }
        //}

        //public string DatabaseName
        //{
        //    get { return Installation.DatabaseName; }
        //}

        //public string DatabaseServer
        //{
        //    get { return Installation.DatabaseServer; }
        //}

        //public IEnumerable<Host> Hosts
        //{
        //    get { return Installation.Hosts.Cast<Host>(); }
        //}
    }
}