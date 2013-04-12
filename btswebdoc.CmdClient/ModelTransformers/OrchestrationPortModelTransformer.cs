using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;
using btswebdoc.Shared.Logging;
using log4net.Repository.Hierarchy;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class OrchestrationPortModelTransformer
    {
        internal static OrchestrationPort TransformModel(Microsoft.BizTalk.ExplorerOM.OrchestrationPort omOrchestrationPort)
        {
            var port = new OrchestrationPort();

            port.Name = omOrchestrationPort.Name;

            return port;
        }

        internal static void SetReferences(OrchestrationPort port, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.OrchestrationPort omPort)
        {

            if (omPort != null)
            {
                if (omPort.ReceivePort != null && artifacts.ReceivePorts.ContainsKey(omPort.ReceivePort.Id()))
                {
                    port.ReceivePort = artifacts.ReceivePorts[omPort.ReceivePort.Id()];
                }
                else if (omPort.SendPort != null && artifacts.SendPorts.ContainsKey(omPort.SendPort.Id()))
                {
                    port.SendPort = artifacts.SendPorts[omPort.SendPort.Id()];
                }
            }
        }
    }
}
