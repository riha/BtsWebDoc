using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using btswebdoc.Model;

namespace btswebdoc.Web.DocsReaders
{
    public static class ManifestReader
    {
        public static Manifest GetCurrentManifest(string version, string applicationRootPath)
        {
            Manifest manifest;

            var manifests = GetAllManifests(applicationRootPath).ToList();

            if (manifests == null || manifests.Count < 1)
                throw new ConfigurationErrorsException(string.Concat("Can locate a valid manifest file in location: ",
                                                                     DocsExportFolderManager.GetDocsExportFolderPath(applicationRootPath)));

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
           if(!Directory.Exists(DocsExportFolderManager.GetDocsExportFolderPath(path)))
               throw new ConfigurationErrorsException(string.Concat("Can not find a folder containing exported documentation at the following location: ", path, Environment.NewLine, "BizTalk Web Documenter will by default look for a folder namned 'Docs' in the applications root folder. This location can however also be overriden  and set to a specific location in the web.config."));

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