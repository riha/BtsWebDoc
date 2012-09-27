using System.Web;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class SchemaExtensions
    {
        public static string SourcePath(this Schema schema, Manifest manifest)
        {
            var versionPrefix = string.Empty;
            var reuqestPath = HttpContext.Current.Request.ApplicationPath;
            const string action = "/Schema/";

            if (reuqestPath != "/")
                reuqestPath = reuqestPath + "/";

            if (manifest != null && !manifest.IsDefaultLatest)
                versionPrefix = manifest.Version + "/";

            return string.Concat(reuqestPath, versionPrefix, "Assets", action, schema.Name, ".xml");
        }
    }
}