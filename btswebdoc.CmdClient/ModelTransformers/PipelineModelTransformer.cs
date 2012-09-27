using btswebdoc.Model;
using btswebdoc.CmdClient.Extensions;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class PipelineModelTransformer
    {
        internal static Pipeline TransformModel(Microsoft.BizTalk.ExplorerOM.Pipeline omPipeline)
        {
            var pipeline = new Pipeline();
            pipeline.Name = omPipeline.FullName;
            pipeline.Type = omPipeline.Type.ToString();
            pipeline.Description = omPipeline.Description;

            return pipeline;
        }

        internal static void SetReferences(Pipeline pipeline, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.Pipeline omPipeline)
        {
            pipeline.Application = artifacts.Applications[omPipeline.Application.Id()];
            pipeline.ParentAssembly = artifacts.Assemblies[omPipeline.BtsAssembly.Id()];
        }
    }
}
