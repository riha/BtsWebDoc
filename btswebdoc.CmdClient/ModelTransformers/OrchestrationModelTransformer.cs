using System.Linq;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class OrchestrationModelTransformer
    {
        internal static Orchestration TransformModel(Microsoft.BizTalk.ExplorerOM.BtsOrchestration omOrchestration)
        {
            var orchestration = new Orchestration();

            orchestration.Name = omOrchestration.FullName;

            orchestration.Description = omOrchestration.Description;

            foreach (Microsoft.BizTalk.ExplorerOM.OrchestrationPort omPort in omOrchestration.Ports)
            {
                orchestration.Ports.Add(OrchestrationPortModelTransformer.TransformModel(omPort));
            }

            return orchestration;
        }

        internal static void SetReferences(Orchestration orchestration, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.BtsOrchestration omOrchestration)
        {
            //As it's possible to exclude application we don't always have all. Only add application we have.
            if (artifacts.Applications.ContainsKey(omOrchestration.Application.Id()))
            {
                orchestration.Application = artifacts.Applications[omOrchestration.Application.Id()];
            }

            //As it's possible to exclude application we don't always have all assemblies. Only add assemblies we have.
            if (artifacts.Assemblies.ContainsKey(omOrchestration.BtsAssembly.Id()))
            {
                orchestration.ParentAssembly = artifacts.Assemblies[omOrchestration.BtsAssembly.Id()];
            }

            if (omOrchestration.Host != null)
            {
                orchestration.Host = artifacts.Hosts[omOrchestration.Host.Id()];
            }

            foreach (var port in orchestration.Ports)
            {
                OrchestrationPortModelTransformer.SetReferences(port, artifacts, omOrchestration.Ports.Cast<Microsoft.BizTalk.ExplorerOM.OrchestrationPort>().Where(o => o.Name == port.Name).SingleOrDefault());
            }
        }
    }
}
