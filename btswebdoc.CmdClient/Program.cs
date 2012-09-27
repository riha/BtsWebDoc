using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using btswebdoc.Model;
using btswebdoc.Shared;
using btswebdoc.Shared.Extensions;
using btswebdoc.Shared.Logging;
using NDesk.Options;

namespace btswebdoc.CmdClient
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                ExceuteProgram(args);
            }
            catch (SqlException exception)
            {
                Log.Error("Exception while trying to establish connection to database. Make sure you have a valid connection to the BizTalk management database.", exception);
            }
            catch (Exception exception)
            {
                Log.Error("Unhandled exception", exception);
            }
        }

        private static void ExceuteProgram(IEnumerable<string> args)
        {
            var sw = Stopwatch.StartNew();

            var p = ParseCommandParameters(args);

            if (p.ShouldShowHelp || !p.ShouldExport)
                ShowHelp();
            else
            {
                var catalogReader = new BtsCatalogReader(p.Server, p.Database, p.ExcludedApplications);

                if (string.IsNullOrEmpty(p.Environment))
                {
                    p.Environment = catalogReader.GroupName;
                }

                var manifest = new Manifest(DateTime.Now, p.Server, p.Database, p.Comment, p.Environment);

                var exportFolderPath = Path.Combine(GetDocsExportFolder(p.Folder), manifest.Version);

                DirectoryHelper.CreateDirectory(exportFolderPath);

                BizTalkArtifacts artifacts = TransformArtifacts(catalogReader);

                ExportArtifacts(catalogReader, manifest, exportFolderPath, artifacts);

                Log.Info("Completed export of BtsWebDoc documentaion to {0} in {1} sec.", exportFolderPath, sw.Elapsed.TotalSeconds);
            }
        }

        private static void ExportArtifacts(BtsCatalogReader catalogReader, Manifest manifest, string exportFolderPath, BizTalkArtifacts artifacts)
        {
            var dataExporter = new ApplicationDataExporter(exportFolderPath);
            dataExporter.ExportApplicationData(artifacts, catalogReader.Applications);

            var assetsExporter = new AssetsExporter(exportFolderPath);
            assetsExporter.ExportMapSources(catalogReader.Transforms);
            assetsExporter.ExportSchemaSources(catalogReader.Schemas);
            assetsExporter.ExportOrchestrationOverviews(catalogReader.Orchestrations);

            manifest.Save(Path.Combine(exportFolderPath, Manifest.FileName));
        }

        private static BizTalkArtifacts TransformArtifacts(BtsCatalogReader catalogReader)
        {
            var artifacts = new BizTalkArtifacts
                                {
                                    Applications = ModelTransformer.TransformApplications(catalogReader.Applications),
                                    Schemas = ModelTransformer.TransformSchemas(catalogReader.Schemas),
                                    Transforms = ModelTransformer.TransformTransforms(catalogReader.Transforms),
                                    SendPorts = ModelTransformer.TransformSendPorts(catalogReader.SendPorts),
                                    ReceivePorts = ModelTransformer.TransformReceivePorts(catalogReader.ReceivePorts),
                                    Assemblies = ModelTransformer.TransformAssemblies(catalogReader.Assemblies),
                                    Orchestrations = ModelTransformer.TransformOrchestrations(catalogReader.Orchestrations),
                                    Pipelines = ModelTransformer.TransformPipelines(catalogReader.Pipelines)
                                };

            ModelReferenceSetter.SetSchemaReferences(artifacts, catalogReader.Schemas);
            ModelReferenceSetter.SetReceivePortReferences(artifacts, catalogReader.ReceivePorts);
            ModelReferenceSetter.SetSendPortReferences(artifacts, catalogReader.SendPorts);
            ModelReferenceSetter.SetAssemblyReferences(artifacts, catalogReader.Assemblies);
            ModelReferenceSetter.SetPipelineReferences(artifacts, catalogReader.Pipelines);
            ModelReferenceSetter.SetTransformReferences(artifacts, catalogReader.Transforms);
            ModelReferenceSetter.SetOrchestrationReferences(artifacts, catalogReader.Orchestrations);

            return artifacts;
        }

        private static CommandParameters ParseCommandParameters(IEnumerable<string> args)
        {
            var parameters = new CommandParameters();

            var p = new OptionSet()
                        {
                            {"export","Starts export of documentaion.", e => parameters.ShouldExport = e != null},    
                            {"help","Show this help and exit.", h => parameters.ShouldShowHelp = h != null},
                            {"database=", "Name of the BizTalk managemenet database to be documented. Default value = \"BizTalkMgmtDb\"." , d => parameters.Database = string.IsNullOrEmpty(d) ? parameters.Database : d},
                            {"server=","Name of the BizTalk management database server. Default value = \".\".",  s => parameters.Server = string.IsNullOrEmpty(s) ? parameters.Server : s},
                            {"folder=","Path to the folder where the documentaion export should be saved. Default value = \"..\\Docs\\\".", f => parameters.Folder = string.IsNullOrEmpty(f) ? parameters.Folder : f},
                            {"comment=" ,"Optional comments to be shown togeteher with the current documentaion version.", c => parameters.Comment = string.IsNullOrEmpty(c) ? parameters.Comment : c},
                            {"environment=" ,"Optional environment name shown (for example 'Test'). Defaults to the name of the BizTalk Group.", e => parameters.Environment = string.IsNullOrEmpty(e) ? parameters.Environment : e},
                            {"exclude=", "Optional list of comma separated list of names of BizTalk applications to exclude from documentation.", ex => parameters.SetExcludedApplications(ex)}
                        };

            p.Parse(args);

            return parameters;
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine(" BizTalk Server Web Documentor, Version " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            Console.WriteLine();
            Console.WriteLine(" client.exe -export [-server] [-database] [-comment] [-environment] [-exclude]");
            Console.WriteLine();
            Console.WriteLine(" -server \t Name of the database server to read the \n\t\t BizTalk Management database from. Defaults to \".\" (localhost).");
            Console.WriteLine(" -database \t Name of the BizTalk Management database.\n\t\t Defaults to \"BizTalkMgmtDb\".");
            Console.WriteLine(" -comment \t Optional comments to be shown togeteher with the \n\t\t current documentaion version.");
            Console.WriteLine(" -environment \t Optional environment name shown (for example 'Test'). \n\t\t Defaults to the name of the BizTalk Group.");
            Console.WriteLine(" folder \t Path to the folder where the documentaion export should be saved. Default value = \"..\\Docs\\\".");
            Console.WriteLine();
            Console.WriteLine(" client.exe -export -server:sqldb123 -database:BizTalkMgmt1");
            Console.WriteLine(" client.exe -export -comment:\"Revision 1234\"");
            Console.WriteLine(" client.exe -export -environment:\"TEST\"");
            Console.WriteLine(" client.exe -export -exclude:\"Application 1, Application 2\"");
            Console.WriteLine();
        }

        private static string GetDocsExportFolder(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                return path;
            }

            return Path.Combine(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName, Constants.DocsFolderName);

        }
    }
}