using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class BizTalkAssemblyModelTransformer
    {
        internal static BizTalkAssembly TransforModel(Microsoft.BizTalk.ExplorerOM.BtsAssembly omAssembly)
        {
            var assembly = new BizTalkAssembly();
            
            assembly.QualifiedName = omAssembly.DisplayName;

            assembly.Name = omAssembly.Name;
            assembly.Version = omAssembly.Version;
            assembly.PublicKeyToken = omAssembly.PublicKeyToken;
            assembly.Culture = omAssembly.Culture;

            return assembly;
        }

        internal static void SetReferences(BizTalkAssembly assembly, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.BtsAssembly omAssembly)
        {
            assembly.Application = artifacts.Applications[omAssembly.Application.Id()];

            foreach (Microsoft.BizTalk.ExplorerOM.BtsOrchestration omOrchestration in omAssembly.Orchestrations)
            {
                assembly.Orchestrations.Add(artifacts.Orchestrations[omOrchestration.Id()]);
            }

            foreach (Microsoft.BizTalk.ExplorerOM.Schema omSchema in omAssembly.Schemas)
            {
                assembly.Schemas.Add(artifacts.Schemas[omSchema.Id()]);
            }

            foreach (Microsoft.BizTalk.ExplorerOM.Transform omTransform in omAssembly.Transforms)
            {
                assembly.Transforms.Add(artifacts.Transforms[omTransform.Id()]);
            }

            foreach (Microsoft.BizTalk.ExplorerOM.Pipeline omPipeline in omAssembly.Pipelines)
            {
                assembly.Pipelines.Add(artifacts.Pipelines[omPipeline.Id()]);
            }
        }
    }
}
