using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class ReceiveLocationModelTransformer
    {
        internal static ReceiveLocation TransforModel(Microsoft.BizTalk.ExplorerOM.ReceiveLocation omReceiveLocation)
        {
            var receiveLocation = new ReceiveLocation();

            receiveLocation.Name = omReceiveLocation.Name;
            receiveLocation.Address = omReceiveLocation.Address;
            receiveLocation.TransportProtocol = omReceiveLocation.TransportType.Name;


            return receiveLocation;
        }

        internal static void SetReferences(ReceiveLocation receiveLocation, Pipeline receivePipeline, Pipeline sendPipeline)
        {
            //receiveLocation.ReceiveHandler = new ReceiveHandler(); //new ReceiveHandler().Map(omReceiveLocation.ReceiveHandler);

            receiveLocation.ReceivePipeline = receivePipeline;
            receiveLocation.SendPipeline = sendPipeline;
        }
    }
}
