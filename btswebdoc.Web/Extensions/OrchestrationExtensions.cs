using System.Web;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class OrchestrationExtensions
    {
        public static string SourcePath(this Orchestration orchestration, Manifest manifest)
        {
            return GetSourceUrl(orchestration, manifest, false);
        }

        public static string ThumbSourcePath(this Orchestration orchestration, Manifest manifest)
        {
            return GetSourceUrl(orchestration, manifest, true);
        }

        private static string GetSourceUrl(this Orchestration orchestration, Manifest manifest, bool isThumb)
        {
            var versionPrefix = string.Empty;
            var reuqestPath = HttpContext.Current.Request.ApplicationPath;
            var action = "/Orchestration/";

            if (reuqestPath != "/")
                reuqestPath = reuqestPath + "/";

            if (manifest != null && !manifest.IsDefaultLatest)
                versionPrefix = manifest.Version + "/";

            if (isThumb)
                action = "/OrchestrationThumb/";

            return string.Concat(reuqestPath, versionPrefix, "Assets", action, orchestration.Name, ".jpeg");
        }
    }
}