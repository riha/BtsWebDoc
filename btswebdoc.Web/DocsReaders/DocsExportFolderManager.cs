using System.Configuration;
using System.IO;
using btswebdoc.Shared;

namespace btswebdoc.Web.DocsReaders
{
    public static class DocsExportFolderManager
    {
        public static string GetDocsExportFolderPath(string path)
        {
            string exportSetting = ConfigurationManager.AppSettings["DocumentsFolder"];

            if (!string.IsNullOrEmpty(exportSetting))
            {
                return exportSetting;
            }

            return Path.Combine(Directory.GetParent(path).Parent.FullName, Constants.DocsFolderName);
        }
    }
}