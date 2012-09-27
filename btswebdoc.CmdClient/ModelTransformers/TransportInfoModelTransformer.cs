using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class TransportInfoModelTransformer
    {
        internal static TransportInfo TransforModel(Microsoft.BizTalk.ExplorerOM.TransportInfo omTransportInfo)
        {
            var transportInfo = new TransportInfo();

            transportInfo.Address = omTransportInfo.Address;
            
            if (omTransportInfo.TransportType != null)
                transportInfo.Type = omTransportInfo.TransportType.Name;

            transportInfo.OrderedDelivery = omTransportInfo.OrderedDelivery;
            transportInfo.RetryCount = omTransportInfo.RetryCount;
            transportInfo.RetryInterval = omTransportInfo.RetryInterval;
            transportInfo.ServiceWindow = ServiceWindowModelTransformer.TransformModel(omTransportInfo);
            return transportInfo;
        }

    }
}
