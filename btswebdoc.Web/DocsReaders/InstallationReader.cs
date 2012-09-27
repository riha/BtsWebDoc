using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using btswebdoc.Model;
using btswebdoc.Web.Extensions;

namespace btswebdoc.Web.DocsReaders
{
    public static class InstallationReader
    {
        public static BizTalkInstallation GetBizTalkInstallation(Manifest manifest)
        {
            return ReadBizTalkInstallation(manifest);
        }

        private static BizTalkInstallation ReadBizTalkInstallation(Manifest manifest)
        {
            var installation = HttpRuntime.Cache.Get(manifest.Version) as BizTalkInstallation;

            if (installation != null)
                return installation;


            installation = new BizTalkInstallation();

            foreach (var file in Directory.EnumerateFiles(Path.Combine(manifest.Path, "Data")))
            {
                using (
                    var fs = new FileStream(Path.Combine(manifest.Path, file), FileMode.Open,
                                            FileAccess.Read, FileShare.None))
                {
                    using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        installation.Applications.Add(Path.GetFileNameWithoutExtension(file), (BizTalkApplication)new BinaryFormatter().Deserialize(gz));
                    }
                }
            }

            HttpRuntime.Cache.Clear();
            HttpRuntime.Cache.Insert(manifest.Version, installation);

            return installation;
        }

    }
}
