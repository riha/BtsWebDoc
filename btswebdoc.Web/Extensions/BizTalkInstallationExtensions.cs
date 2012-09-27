using System;
using System.Linq;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    /// <summary>
    /// Extension to the installation object that holds a list of all artefatcs. Has methods to lookup specific artefatcs within each list.
    /// </summary>
    public static class BizTalkInstallationExtensions
    {
        //public static Orchestration GetOrchestration(this BizTalkInstallation installation, string name)
        //{
        //        return installation.Orchestrations.First(o => o.ReferanceKey == name);
        //}

        //public static BizTalkAssembly GetAssembly(this BizTalkInstallation installation, string id)
        //{
        //    return installation.Assemblies.First(a => a.ReferanceKey == id);
        //}

        //public static Host GetHost(this BizTalkInstallation installation, string name)
        //{
        //    return null;
        //    //return installation.Hosts.Cast<Host>().First(h => h.Name == name);
        //}

        //public static Transform GetTransform(this BizTalkInstallation installation, string name)
        //{
        //    return installation.Transforms.First(t => t.ReferanceKey == name);
        //}

        //public static ReceivePort GetReceivePort(this BizTalkInstallation installation, string name)
        //{
        //    return installation.ReceivePorts.First(rp => rp.ReferanceKey == name);
        //}

        //public static SendPort GetSendPort(this BizTalkInstallation installation, string name)
        //{
        //    return installation.SendPorts.First(sp => sp.ReferanceKey == name);
        //}

        //public static Schema GetSchema(this BizTalkInstallation installation, string id)
        //{
        //    return installation.Schemas.First(s => s.ReferanceKey == id);
        //}

        //public static Schema GetSchemaByUniqueName(this BizTalkInstallation installation, string id)
        //{
        //    return installation.Schemas.First(s => s.ReferanceKey == id);
        //}

        //public static BizTalkApplication GetApplication(this BizTalkInstallation installation, string name)
        //{
        //        return installation.Applications.First(a => a.ReferanceKey == name);
        //}

        //public static Pipeline GetPipeline(this BizTalkInstallation installation, string name)
        //{
        //    return installation.Pipelines.First(p => p.ReferanceKey == name);
        //}

    }
}