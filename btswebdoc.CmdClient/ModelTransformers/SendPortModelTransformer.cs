﻿using System.Collections.Generic;
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
            var sendPort = new SendPort();

            sendPort.Name = omSendPort.Name;
            sendPort.Description = omSendPort.Description;
            sendPort.FilterGroups = FilterGroupTransformer.CreateFilterGroups(omSendPort.Filter);
            sendPort.Priority = omSendPort.Priority;
            sendPort.TrackingType = omSendPort.Tracking.ToString();
            sendPort.RouteFailedMessage = omSendPort.RouteFailedMessage;
            sendPort.StopSendingOnFailure = omSendPort.StopSendingOnFailure;
            sendPort.Dynamic = omSendPort.IsDynamic;
            sendPort.TwoWay = omSendPort.IsTwoWay;

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
