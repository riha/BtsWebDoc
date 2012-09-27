using System.Web;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class TransformExtension
    {
        public static string SourcePath(this Transform transform, Manifest manifest)
        {
            var versionPrefix = string.Empty;
            var reuqestPath = HttpContext.Current.Request.ApplicationPath;
            const string action = "/Map/";

            if (reuqestPath != "/")
                reuqestPath = reuqestPath + "/";

            if (manifest != null && !manifest.IsDefaultLatest)
                versionPrefix = manifest.Version + "/";

            return string.Concat(reuqestPath, versionPrefix, "Assets", action, transform.Name, ".xml");
        }
    }
}