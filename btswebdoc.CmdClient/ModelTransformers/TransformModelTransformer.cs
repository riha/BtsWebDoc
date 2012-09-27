using System.Linq;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class TransformModelTransformer
     {
        internal static Transform TransformModel(Microsoft.BizTalk.ExplorerOM.Transform omTransform)
        {
            var transform = new Transform();

            transform.Name = omTransform.FullName;

            transform.Description = omTransform.Description;

            return transform;
        }

        internal static void SetReferences(Transform transform, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.Transform omTransform)
        {
            transform.Application = artifacts.Applications[omTransform.Application.Id()];
            transform.ParentAssembly = artifacts.Assemblies[omTransform.BtsAssembly.Id()];

            //As it's possible to exclude application we don't always have all schemas. Only add source schema if we have.
            if (artifacts.Schemas.ContainsKey(omTransform.SourceSchema.Id()))
            {
                transform.SourceSchema = artifacts.Schemas[omTransform.SourceSchema.Id()]; ;
            }

            //As it's possible to exclude application we don't always have all schemas. Only add target schema if we have.
            if (artifacts.Schemas.ContainsKey(omTransform.TargetSchema.Id()))
            {
                transform.TargetSchema = artifacts.Schemas[omTransform.TargetSchema.Id()];
            }

            transform.ReceivePorts.AddRange(
                artifacts.ReceivePorts.Where(rp => rp.Value.InboundTransforms.Select(t => t.Id).Contains(transform.Id)).Select(
                    rp => rp.Value));

            transform.SendPorts.AddRange(
            artifacts.SendPorts.Where(rp => rp.Value.OutboundTransforms.Select(t => t.Id).Contains(transform.Id)).Select(
                rp => rp.Value));
        }
    }
}
