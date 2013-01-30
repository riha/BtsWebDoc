using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using btswebdoc.Model;
using btswebdoc.Shared.Exceptions;

namespace btswebdoc.Web.DocsReaders
{
    public static class ManifestReader
    {
        public static Manifest GetCurrentManifest(string version, string applicationRootPath)
        {
            Manifest manifest;

            var manifests = GetAllManifests(applicationRootPath).ToList();

            if (manifests.Count < 1)
            {
                string path = DocsExportFolderManager.GetDocsExportFolderPath(applicationRootPath);
                throw new MissingDocumentationException(string.Concat("Error locating documentation in: ", path)) { Path = path };
            }

            if (string.IsNullOrEmpty(version))
            {
                manifest = manifests.Single(d => d.Version == manifests.Select(m => m.Version).Max());
                manifest.IsDefaultLatest = true;
            }
            else
            {
                manifest = manifests.Single(d => d.Version.Equals(version));
            }

            return manifest;
        }


        public static IEnumerable<Manifest> GetAllManifests(string path)
        {
            if (!Directory.Exists(DocsExportFolderManager.GetDocsExportFolderPath(path)))
                throw new MissingDocumentationException(string.Concat("Error locating documentation in: ", path)) { Path = path };

            var exportDirectories = Directory.GetDirectories(DocsExportFolderManager.GetDocsExportFolderPath(path));

            var manifests = new List<Manifest>();

            foreach (var directory in exportDirectories)
            {
                string manifestFilePath = Path.Combine(directory, Manifest.FileName);

                if (File.Exists(manifestFilePath))
                {
                    var manifest = Manifest.Load(manifestFilePath);
                    manifest.Path = directory;

                    manifests.Add(manifest);
                }
            }

            return manifests.OrderByDescending(m => m.Version);
        }
    }
}