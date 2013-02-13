using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.CmdClient.ModelTransformers;
using btswebdoc.Shared.Logging;

namespace btswebdoc.CmdClient
{
    class ModelReferenceSetter
    {
        internal static void SetSchemaReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.Schema> schemas)
        {
            Log.Info("Sets model references for schemas");

            var transformer = new SchemaModelTransformer();
            foreach (var omSchema in schemas)
            {
                Log.Debug("Sets references for schema {0}", omSchema.FullName);
                var schema = artifacts.Schemas[omSchema.Id()];
                transformer.SetReferences(schema, artifacts, omSchema);
            }
        }

        internal static void SetReceivePortReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.ReceivePort> receivePorts)
        {
            Log.Info("Sets model references for receive ports");

            foreach (var omReceivePort in receivePorts)
            {
                Log.Debug("Sets references for receive port {0}", omReceivePort.Name);
                var receivePort = artifacts.ReceivePorts[omReceivePort.Id()];
                ReceivePortModelTransformer.SetReferences(receivePort, artifacts, omReceivePort);
            }
        }

        internal static void SetSendPortReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.SendPort> sendPorts)
        {
            Log.Info("Sets model references for send ports");

            foreach (var omSendPort in sendPorts)
            {
                Log.Debug("Sets references for send port {0}", omSendPort.Name);
                var sendPort = artifacts.SendPorts[omSendPort.Id()];
                SendPortModelTransformer.SetReferences(sendPort, artifacts, omSendPort);
            }
        }

        internal static void SetSendPortGroupReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.SendPortGroup> sendPortsGroup)
        {
            Log.Info("Sets model references for send ports groups");

            foreach (var omSendPortGroup in sendPortsGroup)
            {
                Log.Debug("Sets references for send port group {0}", omSendPortGroup.Name);
                var sendPortGroup = artifacts.SendPortGroups[omSendPortGroup.Id()];
                SendPortGroupModelTransformer.SetReferences(sendPortGroup, artifacts, omSendPortGroup);
            }
        }

        internal static void SetAssemblyReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsAssembly> omAssemblies)
        {
            Log.Info("Sets model references for assemblies");

            foreach (var omAssembly in omAssemblies)
            {
                Log.Debug("Sets references for assembly {0}", omAssembly.DisplayName);
                var assembly = artifacts.Assemblies[omAssembly.Id()];
                BizTalkAssemblyModelTransformer.SetReferences(assembly, artifacts, omAssembly);
            }
        }


        internal static void SetTransformReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.Transform> omTransforms)
        {
            Log.Info("Sets model references for maps");

            foreach (var omTransform in omTransforms)
            {
                Log.Debug("Sets references for map {0}", omTransform.FullName);
                var transform = artifacts.Transforms[omTransform.Id()];
                TransformModelTransformer.SetReferences(transform, artifacts, omTransform);
            }
        }

        internal static void SetOrchestrationReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsOrchestration> omOrchestrations)
        {
            Log.Info("Sets model references for orchestrations");

            foreach (var omOrchestration in omOrchestrations)
            {
                Log.Debug("Sets references for orchestration {0}", omOrchestration.FullName);
                var orchestration = artifacts.Orchestrations[omOrchestration.Id()];
                OrchestrationModelTransformer.SetReferences(orchestration, artifacts, omOrchestration);
            }
        }

        internal static void SetPipelineReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.Pipeline> omPipelines)
        {
            Log.Info("Sets model references for pipelines");

            foreach (var omPipeline in omPipelines)
            {
                Log.Debug("Sets references for pipeline {0}", omPipeline.FullName);
                var pipeline = artifacts.Pipelines[omPipeline.Id()];
                PipelineModelTransformer.SetReferences(pipeline, artifacts, omPipeline);
            }
        }

        internal static void SetHostReferences(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.Host> omHosts, 
            IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsOrchestration> omOrchestrations,
            IEnumerable<Microsoft.BizTalk.ExplorerOM.SendPort> omSendPorts,
            IEnumerable<Microsoft.BizTalk.ExplorerOM.ReceivePort> omReceivePorts)
        {
            Log.Info("Sets model references for hosts");

            foreach (var omHost in omHosts)
            {
                Log.Debug("Sets references for host {0}", omHost.Name);
                var host = artifacts.Hosts[omHost.Id()];
                HostModelTransformer.SetReferences(host, artifacts, omOrchestrations, omSendPorts, omReceivePorts);
            }
        }
    }
}
