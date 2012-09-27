using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class ServiceWindowModelTransformer
    {
        internal static ServiceWindow TransformModel(Microsoft.BizTalk.ExplorerOM.TransportInfo omTransportInfo)
        {
            var serviceWindow = new ServiceWindow();

            serviceWindow.Enabled = omTransportInfo.ServiceWindowEnabled;
            serviceWindow.FromTime = omTransportInfo.FromTime;
            serviceWindow.ToTime = omTransportInfo.ToTime;

            return serviceWindow;
        }


    }
}
