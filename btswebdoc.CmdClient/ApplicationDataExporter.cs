using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;
using btswebdoc.CmdClient.Extensions;

using btswebdoc.CmdClient.ModelTransformers;
using btswebdoc.Shared.Extensions;
using btswebdoc.Shared.Logging;

namespace btswebdoc.CmdClient
{
    /// <summary>
    /// Responsible for mapping between the ExplorerOM types and the btswebdoc types. Also uses the AssestExport class to write all artefact files to disc.
    /// </summary>
    internal class ApplicationDataExporter
    {
        private readonly string _exportPath;
        private readonly string _dataExportPath;

        public ApplicationDataExporter(string exportPath)
        {
            _exportPath = exportPath;
            _dataExportPath = DirectoryHelper.CreateDirectory(Path.Combine(_exportPath, "Data"));
        }

        public void ExportApplicationData(BizTalkArtifacts artifacts, IEnumerable<Microsoft.BizTalk.ExplorerOM.Application> omApplications)
        {
            Log.Info("Sets model references and serializes applications");

            foreach (var omApplication in omApplications)
            {
                var application = artifacts.Applications[omApplication.Id()];

                Log.Debug(string.Format("Sets references references for application {0}", application.Name));

                foreach (Microsoft.BizTalk.ExplorerOM.Application omReferencingApplication in omApplication.References)
                {
                    if (artifacts.Applications.ContainsKey(omReferencingApplication.Id()))
                        application.ReferencedApplications.Add(artifacts.Applications[omReferencingApplication.Id()]);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.BtsOrchestration omOrchestration in omApplication.Orchestrations)
                {
                    var orchestration = artifacts.Orchestrations[omOrchestration.Id()];

                    if (!application.Orchestrations.ContainsKey(orchestration.Id))
                        application.Orchestrations.Add(orchestration.Id, orchestration);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.Schema omSchema in omApplication.Schemas)
                {
                    var schema = artifacts.Schemas[omSchema.Id()];

                    if (!application.Schemas.ContainsKey(schema.Id))
                        application.Schemas.Add(schema.Id, schema);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.Transform omTransform in omApplication.Transforms)
                {
                    var transform = artifacts.Transforms[omTransform.Id()];

                    if (!application.Maps.ContainsKey(transform.Id))
                        application.Maps.Add(transform.Id, transform);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.ReceivePort omReceivePort in omApplication.ReceivePorts)
                {
                    var receivePort = artifacts.ReceivePorts[omReceivePort.Id()];

                    if (!application.ReceivePorts.ContainsKey(receivePort.Id))
                        application.ReceivePorts.Add(receivePort.Id, receivePort);
                }


                foreach (Microsoft.BizTalk.ExplorerOM.SendPort omSendPort in omApplication.SendPorts)
                {
                    var sendPort = artifacts.SendPorts[omSendPort.Id()];

                    if (!application.SendPorts.ContainsKey(sendPort.Id))
                        application.SendPorts.Add(sendPort.Id, sendPort);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.SendPortGroup omSendPortGroup in omApplication.SendPortGroups)
                {
                    var sendPortGroup = artifacts.SendPortGroups[omSendPortGroup.Id()];

                    if (!application.SendPortGroups.ContainsKey(sendPortGroup.Id))
                        application.SendPortGroups.Add(sendPortGroup.Id, sendPortGroup);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.BtsAssembly omAssembly in omApplication.Assemblies)
                {
                    var assembly = artifacts.Assemblies[omAssembly.Id()];

                    if (!application.Assemblies.ContainsKey(assembly.Id))
                        application.Assemblies.Add(assembly.Id, assembly);
                }

                foreach (Microsoft.BizTalk.ExplorerOM.Pipeline omPipeline in omApplication.Pipelines)
                {
                    var pipeline = artifacts.Pipelines[omPipeline.Id()];

                    if (!application.Pipelines.ContainsKey(pipeline.Id))
                        application.Pipelines.Add(pipeline.Id, pipeline);
                }

                Log.Debug("Serializes Application {0} to disk", application.Id);

                using (var fs = new FileStream(Path.Combine(_dataExportPath, application.Id + ".gz"), FileMode.Create))
                using (var gz = new GZipStream(fs, CompressionMode.Compress))
                {
                    new BinaryFormatter().Serialize(gz, application);
                }
            }
        }
    }
}