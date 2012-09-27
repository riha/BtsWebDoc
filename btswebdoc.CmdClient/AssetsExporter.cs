using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Shared.Extensions;
using btswebdoc.Shared.Logging;
using Microsoft.BizTalk.ExplorerOM;
using Schema = Microsoft.BizTalk.ExplorerOM.Schema;
using Transform = Microsoft.BizTalk.ExplorerOM.Transform;
using System.Xml;

namespace btswebdoc.CmdClient
{
    /// <summary>
    /// Used for exporing all map, schema and orchestrations files
    /// </summary>
    public class AssetsExporter
    {
        private readonly HashSet<string> _generatedMaps = new HashSet<string>();
        private readonly HashSet<string> _generatedOrchestrations = new HashSet<string>();
        private readonly HashSet<string> _generatedSchemas = new HashSet<string>();
        private readonly string _orchestrationExportPath;
        private readonly string _mapExportPath;
        private readonly string _schemaExportPath;

        public AssetsExporter(string assestsExportPath)
        {
            _orchestrationExportPath = DirectoryHelper.CreateDirectory(Path.Combine(assestsExportPath, "Assets/Overview"));
            _mapExportPath = DirectoryHelper.CreateDirectory(Path.Combine(assestsExportPath, "Assets/Maps"));
            _schemaExportPath = DirectoryHelper.CreateDirectory(Path.Combine(assestsExportPath, "Assets/Schemas"));
        }

        public void ExportMapSources(IEnumerable<Transform> omTransforms)
        {
            Log.Info("Exports maps");

            foreach (var omTransform in omTransforms)
            {
                if (!_generatedMaps.Contains(omTransform.FullName))
                {
                    var filePath = Path.Combine(_mapExportPath, string.Concat(omTransform.FullName, ".gz"));

                    Log.Debug("Exports source for map {0}", omTransform.FullName);

                    PersistEncodedXmlAssest(omTransform.GetXmlContent(), filePath);

                    _generatedMaps.Add(omTransform.FullName);
                }

            }
        }


        public void ExportOrchestrationOverviews(IEnumerable<BtsOrchestration> omOrchestrations)
        {
            Log.Info("Exports orchestrations");

            foreach (var omOrchestration in omOrchestrations)
            {
                if (!_generatedOrchestrations.Contains(omOrchestration.FullName))
                {
                    try
                    {
                        var filePath = Path.Combine(_orchestrationExportPath, string.Concat(omOrchestration.FullName, ".jpg"));

                        Log.Debug("Exports overview image for orchestration {0}", omOrchestration.FullName);

                        var orchestrationOverviewImage = new OrchestrationOverviewImage(omOrchestration);
                        using (var image = orchestrationOverviewImage.GetImage())
                        {
                            image.Save(filePath);
                            using (var thumbnail = image.GetThumbnailImage(150, 150, null, IntPtr.Zero))
                            {
                                thumbnail.Save(Path.Combine(_orchestrationExportPath, string.Concat(omOrchestration.FullName, "_thumb.jpg")));
                            }
                        }

                        _generatedOrchestrations.Add(omOrchestration.FullName);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(string.Format("Couldn't generate image for orchestration {0}", omOrchestration.FullName), ex);
                    }
                }
            }
        }

        public void ExportSchemaSources(IEnumerable<Schema> omSchemas)
        {
            Log.Info("Exports schemas");

            foreach (var omSchema in omSchemas)
            {
                if (!_generatedSchemas.Contains(omSchema.FullName))
                {
                    var filePath = Path.Combine(_schemaExportPath, string.Concat(omSchema.FullName, ".gz"));

                    Log.Debug("Exports source for schema {0}", omSchema.FullName);

                    PersistEncodedXmlAssest(omSchema.GetXmlContent(), filePath);

                    _generatedSchemas.Add(omSchema.FullName);
                }
            }
        }

        private static void PersistEncodedXmlAssest(string content, string filePath)
        {
            using (var file = File.Create(filePath))
            using (var gz = new GZipStream(file, CompressionMode.Compress))
            {
                using(var reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings(){IgnoreWhitespace = true, }))
                {
                    //Read the file and figure out the XML encoding to make 
                    //sure we write the file in the correct encoding (UTF-8/UTF-16).
                    Encoding encoding = Encoding.Unicode;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.XmlDeclaration)
                        {
                            var encodingName = reader.GetAttribute("encoding");
                            
                            if(!string.IsNullOrEmpty(encodingName))
                                encoding = Encoding.GetEncoding(encodingName);

                            break;
                        }
                    }


                    using (var sw = new StreamWriter(gz, encoding))
                        sw.Write(content);
                }
            }
        }
    }
}
