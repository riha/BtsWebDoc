using System.Collections.Generic;
using System.Linq;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class HostModelTransformer
    {
        internal static Host TransformModel(Microsoft.BizTalk.ExplorerOM.Host omHost)
        {
            var host = new Host();

            host.Name = omHost.Name;
            host.NTGroupName = omHost.NTGroupName;
            host.Type = omHost.Type.Transform();
            
            return host;
        }
        internal static void SetReferences(Host host, BizTalkArtifacts artifacts,
            IEnumerable<Microsoft.BizTalk.ExplorerOM.BtsOrchestration> omOrchestrations,
            IEnumerable<Microsoft.BizTalk.ExplorerOM.SendPort> omSendPorts,
            IEnumerable<Microsoft.BizTalk.ExplorerOM.ReceivePort> omReceivePorts)
        {
            foreach (var omOrchestration in omOrchestrations.Where(o => o.Host.Id() == host.Id))
            {
                host.Orchestrations.Add(artifacts.Orchestrations[omOrchestration.Id()]);
            }

            foreach (var omSendPort in omSendPorts.Where(sp => (sp.PrimaryTransport != null && sp.PrimaryTransport.SendHandler.Host.Id() == host.Id) || (sp.SecondaryTransport != null && sp.SecondaryTransport.SendHandler != null && sp.SecondaryTransport.SendHandler.Host.Id() == host.Id)))
            {
                host.SendPorts.Add(artifacts.SendPorts[omSendPort.Id()]);
            }

            foreach (var omReceivePort in omReceivePorts)
            {
                var omReceiveLocations = omReceivePort.ReceiveLocations.Cast<Microsoft.BizTalk.ExplorerOM.ReceiveLocation>();

                foreach (var omReceiveLocation in omReceiveLocations.Where(rp => rp.ReceiveHandler.Host.Id() == host.Id))
                {
                    var rp = artifacts.ReceivePorts[omReceiveLocation.ReceivePort.Id()];
                    host.ReceiveLocations.AddRange(rp.ReceiveLocations.Where(rl => rl.Id == omReceiveLocation.Id()));
                }
            }


            //Serviice instances

        }
    }
}
