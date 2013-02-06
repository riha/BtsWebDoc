using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using btswebdoc.Model;
using btswebdoc.Shared;
using btswebdoc.Web.Extensions;

namespace btswebdoc.Web.DocsReaders
{
    /// <summary>
    /// Reads the serlialized model that we written to disk and caches it
    /// </summary>
    public static class InstallationReader
    {
        public static BizTalkInstallation GetBizTalkInstallation(Manifest manifest)
        {
            return ReadBizTalkInstallation(manifest);
        }

        private static BizTalkInstallation ReadBizTalkInstallation(Manifest manifest)
        {
            //We'll add the current version that has been read to cache so we can get at it quicker
            var installation = HttpRuntime.Cache.Get(manifest.Version) as BizTalkInstallation;

            //If we have it in cache we'll use that
            if (installation != null)
            {
                return installation;
            }

            installation = new BizTalkInstallation();

            ReadSerializedArtefacts(installation, manifest, Constants.ApplicationDataPath, AddApplication);
            ReadSerializedArtefacts(installation, manifest, Constants.HostDataPath, AddHost);

            HttpRuntime.Cache.Clear();
            HttpRuntime.Cache.Insert(manifest.Version, installation);

            return installation;
        }

        private static void ReadSerializedArtefacts(BizTalkInstallation installation, Manifest manifest, string artefactPath, Action<BizTalkInstallation, string, GZipStream> artefactsToAdd)
        {
            foreach (var file in Directory.EnumerateFiles(Path.Combine(manifest.Path, artefactPath)))
            {
                using (var fs = new FileStream(Path.Combine(manifest.Path, file), FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using (var gz = new GZipStream(fs, CompressionMode.Decompress))
                    {
                        artefactsToAdd(installation, file, gz);
                    }
                }
            }
        }

        private static void AddHost(BizTalkInstallation installation, string file, GZipStream gz)
        {
            installation.Hosts.Add(Path.GetFileNameWithoutExtension(file), (Host)new BinaryFormatter().Deserialize(gz));
        }

        private static void AddApplication(BizTalkInstallation installation, string file, GZipStream gz)
        {
            installation.Applications.Add(Path.GetFileNameWithoutExtension(file), (BizTalkApplication)new BinaryFormatter().Deserialize(gz));
        }

    }
}
