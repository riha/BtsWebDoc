using System.Collections.Generic;
using System.Linq;
using System.Xml;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class SendPortModelTransformer
    {
        internal static SendPort TransformModel(Microsoft.BizTalk.ExplorerOM.SendPort omSendPort)
        {
            var sendPort = new SendPort
                               {
                                   Name = omSendPort.Name,
                                   Description = omSendPort.Description,
                                   FilterGroups = FilterGroupTransformer.CreateFilterGroups(omSendPort.Filter),
                                   Priority = omSendPort.Priority,
                                   TrackingTypes = (TrackingTypes)omSendPort.Tracking,
                                   RouteFailedMessage = omSendPort.RouteFailedMessage,
                                   StopSendingOnFailure = omSendPort.StopSendingOnFailure,
                                   Dynamic = omSendPort.IsDynamic,
                                   TwoWay = omSendPort.IsTwoWay
                               };

            if (omSendPort.PrimaryTransport != null)
            {
                sendPort.PrimaryTransport = TransportInfoModelTransformer.TransforModel(omSendPort.PrimaryTransport);
            }

            if (omSendPort.SecondaryTransport != null)
            {
                sendPort.SecondaryTransport = TransportInfoModelTransformer.TransforModel(omSendPort.SecondaryTransport);
            }

            return sendPort;
        }

        internal static void SetReferences(SendPort sendPort, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.SendPort omSendPort)
        {
            sendPort.Application = artifacts.Applications[omSendPort.Application.Id()];
            sendPort.SendPipeline = artifacts.Pipelines[omSendPort.SendPipeline.Id()];

            // Handles dynamic send ports as these don't have a send handler configured 
            if (omSendPort.PrimaryTransport != null)
            {
                sendPort.PrimaryTransport.Host = artifacts.Hosts[omSendPort.PrimaryTransport.SendHandler.Host.Id()];
            }

            if (omSendPort.SecondaryTransport != null && omSendPort.SecondaryTransport.SendHandler != null)
            {
                sendPort.SecondaryTransport.Host = artifacts.Hosts[omSendPort.SecondaryTransport.SendHandler.Host.Id()];
            }

            if (omSendPort.InboundTransforms != null)
            {
                var inboundIds = omSendPort.InboundTransforms.Cast<Microsoft.BizTalk.ExplorerOM.Transform>().Select(s => s.Id());
                sendPort.InboundTransforms.AddRange(artifacts.Transforms.Where(t => inboundIds.Contains(t.Key)).Select(s => s.Value));
            }

            if (omSendPort.OutboundTransforms != null)
            {
                var inboundIds = omSendPort.OutboundTransforms.Cast<Microsoft.BizTalk.ExplorerOM.Transform>().Select(s => s.Id());
                sendPort.InboundTransforms.AddRange(artifacts.Transforms.Where(t => inboundIds.Contains(t.Key)).Select(s => s.Value));

            }

            var orchestrations = artifacts.Orchestrations.Where(
                o =>
                o.Value.Ports.Where(p => p.SendPort != null).Select(p => p.SendPort.Id).Contains(omSendPort.Id())).
                Select(o => o.Value);

            sendPort.BoundOrchestrations.AddRange(orchestrations);
        }
    }
}
