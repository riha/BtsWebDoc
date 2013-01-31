using System.Linq;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class ReceivePortModelTransformer
    {
        internal static ReceivePort TransforModel(Microsoft.BizTalk.ExplorerOM.ReceivePort omReceivePort)
        {
            var receivePort = new ReceivePort();

            receivePort.Name = omReceivePort.Name;
            receivePort.TrackingTypes = (TrackingTypes)omReceivePort.Tracking;
            receivePort.Description = omReceivePort.Description;
            receivePort.AuthenticationType = (AuthenticationType)omReceivePort.Authentication;

            return receivePort;
        }

        internal static void SetReferences(ReceivePort receivePort, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.ReceivePort omReceivePort)
        {
            receivePort.Application = artifacts.Applications[omReceivePort.Application.Id()];

            foreach (Microsoft.BizTalk.ExplorerOM.ReceiveLocation omReceiveLocation in omReceivePort.ReceiveLocations)
            {
                var receiveLocation = ReceiveLocationModelTransformer.TransforModel(omReceiveLocation);
                var receivePipeline = omReceiveLocation.ReceivePipeline != null ? artifacts.Pipelines[omReceiveLocation.ReceivePipeline.Id()] : null;
                var sendPipeline = omReceiveLocation.SendPipeline != null ? artifacts.Pipelines[omReceiveLocation.SendPipeline.Id()] : null;
                ReceiveLocationModelTransformer.SetReferences(receiveLocation, receivePipeline, sendPipeline);

                receivePort.ReceiveLocations.Add(receiveLocation);
            }

            if (omReceivePort.InboundTransforms != null)
            {
                var inboundIds = omReceivePort.InboundTransforms.Cast<Microsoft.BizTalk.ExplorerOM.Transform>().Select(s => s.Id());
                receivePort.InboundTransforms.AddRange(artifacts.Transforms.Where(t => inboundIds.Contains(t.Key)).Select(s => s.Value));
            }

            if (omReceivePort.OutboundTransforms != null)
            {
                var outboundIds = omReceivePort.OutboundTransforms.Cast<Microsoft.BizTalk.ExplorerOM.Transform>().Select(s => s.Id());
                receivePort.OutboundTransforms.AddRange(artifacts.Transforms.Where(t => outboundIds.Contains(t.Key)).Select(t => t.Value));
            }

            receivePort.BoundOrchestrations.AddRange(artifacts.Orchestrations.Where(o => o.Value.Ports.Where(p => p.SendPort != null).Select(p => p.SendPort.Id).Contains(omReceivePort.Id())).Select(o => o.Value));
        }
    }
}
