using System.Collections.Generic;
using btswebdoc.CmdClient.Extensions;

using btswebdoc.CmdClient.ModelTransformers;
using btswebdoc.Model;
using btswebdoc.Shared.Logging;

namespace btswebdoc.CmdClient
{
    class ModelTransformer
    {
        internal static IDictionary<string, Schema> TransformSchemas(IEnumerable<Microsoft.BizTalk.ExplorerOM.Schema> omSchemas)
        {
            Log.Info("Transforms schemas in new model");

            var schemas = new Dictionary<string, Schema>();

            var transformer = new SchemaModelTransformer();

            foreach (var omSchema in omSchemas)
            {
                if (!schemas.ContainsKey(omSchema.Id()))
                {
                    Log.Debug("Tranform schema '{0}' into new model", omSchema.FullName);
                    schemas.Add(omSchema.Id(), transformer.TransformModel(omSchema));
                }
                else
                {
                    Log.Warn("Skips schema '{0}' as it exists in model", omSchema.FullName);
                }
            }

            return schemas;
        }

        internal static IDictionary<string, Transform> TransformTransforms(IEnumerable<Microsoft.BizTalk.ExplorerOM.Transform> omTransforms)
        {
            Log.Info("Tranforms maps in new model");

            var transforms = new Dictionary<string, Transform>();

            foreach (var omTransform in omTransforms)
            {
                if (!transforms.ContainsKey(omTransform.Id()))
                {
                    Log.Debug("Tranform map '{0}' into new model", omTransform.FullName);
                    transforms.Add(omTransform.Id(), TransformModelTransformer.TransformModel(omTransform));
                }
                else
                {
                    Log.Warn("Skips map '{0}' as it exists in model", omTransform.FullName);
                }
            }

            return transforms;
        }

        internal static IDictionary<string, SendPort> TransformSendPorts(IEnumerable<Microsoft.BizTalk.ExplorerOM.SendPort> omSendPorts)
        {
            Log.Info("Tranforms send ports to new model");

            var sendPorts = new Dictionary<string, SendPort>();

            foreach (var omSendPort in omSendPorts)
            {
                if (!sendPorts.ContainsKey(omSendPort.Id()))
                {
                    Log.Debug("Tranform send port '{0}' into new model", omSendPort.Name);
                    sendPorts.Add(omSendPort.Id(), SendPortModelTransformer.TransformModel(omSendPort));
                }
                else
                {
                    Log.Warn("Skips send port '{0}' as it exists in model", omSendPort.Name);
                }
            }

            return sendPorts;
        }

        internal static IDictionary<string, ReceivePort> TransformReceivePorts(IEnumerable<Microsoft.BizTalk.ExplorerOM.ReceivePort> omReceivePorts)
        {
            Log.Info("Tranforms receive ports in new model");

            var reveicePorts = new Dictionary<string, ReceivePort>();

            foreach (var omReceivePort in omReceivePorts)
            {
                if (!reveicePorts.ContainsKey(omReceivePort.Id()))
                {
                    Log.Debug("Tranform receive port '{0}' into new model", omReceivePort.Name);
                    reveicePorts.Add(omReceivePort.Id(), ReceivePortModelTransformer.TransforModel(omReceivePort));
                }
                else
                {
                    Log.Warn("Skips receive port '{0}' as it exists in model", omReceivePort.Name);
                }
            }

            return reveicePorts;
        }

        internal static IDictionary<string, BizTalkAssembly> TransformAssemblies(IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsAssembly> omAssemblies)
        {
            Log.Info("Tranforms assemblies in new model");

            var assemblies = new Dictionary<string, BizTalkAssembly>();

            foreach (var omAssembly in omAssemblies)
            {
                if (!assemblies.ContainsKey(omAssembly.Id()))
                {
                    Log.Debug("Tranform assembly '{0}' into new model", omAssembly.DisplayName);
                    assemblies.Add(omAssembly.Id(), BizTalkAssemblyModelTransformer.TransforModel(omAssembly));
                }
                else
                {
                    Log.Warn("Skips assembly '{0}' as it exists in model", omAssembly.DisplayName);
                }
            }

            return assemblies;
        }

        internal static IDictionary<string, Orchestration> TransformOrchestrations(IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsOrchestration> omOrchestrations)
        {
            Log.Info("Tranforms orchestrations in new model");

            var orchestrations = new Dictionary<string, Orchestration>();

            foreach (var omOrchestration in omOrchestrations)
            {
                if (!orchestrations.ContainsKey(omOrchestration.Id()))
                {
                    Log.Debug("Tranform orchestration '{0}' into new model", omOrchestration.FullName);
                    orchestrations.Add(omOrchestration.Id(), OrchestrationModelTransformer.TransformModel(omOrchestration));
                }
                else
                {
                    Log.Warn("Skips orchestration '{0}' as it exists in model", omOrchestration.FullName);
                }
            }

            return orchestrations;
        }

        internal static IDictionary<string, Pipeline> TransformPipelines(IEnumerable<Microsoft.BizTalk.ExplorerOM.Pipeline> omPipelines)
        {
            Log.Info("Tranforms pipelines in new model");

            var pipelines = new Dictionary<string, Pipeline>();

            foreach (var omPipeline in omPipelines)
            {
                if (!pipelines.ContainsKey(omPipeline.Id()))
                {
                    Log.Debug("Tranform pipeline '{0}' into new model", omPipeline.FullName);
                    pipelines.Add(omPipeline.Id(), PipelineModelTransformer.TransformModel(omPipeline));
                }
                else
                {
                    Log.Warn("Skips pipeline '{0}' as it exists in model", omPipeline.FullName);
                }
            }

            return pipelines;
        }

        internal static IDictionary<string, BizTalkApplication> TransformApplications(IEnumerable<Microsoft.BizTalk.ExplorerOM.Application> omApplications)
        {
            Log.Info("Tranforms applications in new model");

            var applications = new Dictionary<string, BizTalkApplication>();

            foreach (var omApplication in omApplications)
            {
                if (!applications.ContainsKey(omApplication.Id()))
                {
                    Log.Debug("Tranform application {0} into new model", omApplication.Name);
                    applications.Add(omApplication.Id(), BizTalkApplicationModelTransformer.TransformModel(omApplication));
                }
                else
                {
                    Log.Warn("Skips application {0} as it exists in model", omApplication.Name);
                }
            }

            return applications;
        }
    }
}
