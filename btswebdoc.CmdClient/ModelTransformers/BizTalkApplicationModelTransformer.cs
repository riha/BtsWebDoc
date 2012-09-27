using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class BizTalkApplicationModelTransformer
    {
        internal static BizTalkApplication TransformModel(Microsoft.BizTalk.ExplorerOM.Application omApplication)
        {
            var application = new BizTalkApplication();
            
            application.Name = omApplication.Name;
            application.Description = omApplication.Description;

            return application;
        }
    }
}
