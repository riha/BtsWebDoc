using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Controllers
{
    public class ApplicationController : Controller
    {
        public ActionResult Index(string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject> { application };

            return View(new ApplicationViewModel(
                            application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            application));
        }

        public ActionResult SendPort(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            SendPort sendPort = application.SendPorts[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                      application,
                                      sendPort
                                  };

            return View(new SendPortViewModel(
                application,
                ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            sendPort));
        }


        public ActionResult SendPortGroup(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            SendPortGroup sendPortGroup = application.SendPortGroups[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                      application,
                                      sendPortGroup
                                  };

            return View(new SendPortGroupViewModel(
                application,            
                ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            sendPortGroup));
        }

        public ActionResult ReceivePort(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            ReceivePort receivePort = application.ReceivePorts[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                      application,
                                      receivePort
                                  };

            return View(new ReceivePortViewModel(
                application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            receivePort));
        }

        public ActionResult Orchestration(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            Orchestration orchestration = application.Orchestrations[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                     application,
                                      orchestration
                                  };

            return View(new OrchestrationViewModel(
                application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            orchestration));
        }

        public ActionResult Map(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            Transform transform = application.Maps[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                     application,
                                      transform
                                  };

            return View(new TransformViewModel(
                application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            transform));
        }

        public ActionResult Schema(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            Schema schema = application.Schemas[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                     application,
                                     schema
                                  };

            return View(new SchemaViewModel(
                application,
                        ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                        manifest,
                        breadCrumbs,
                        
                        installation.Applications.Values,
                        installation.Hosts.Values,
                        schema));
        }


        public ActionResult Pipeline(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            Pipeline pipeline = application.Pipelines[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                      application,
                                      pipeline
                                  };

            return View(new PipelineViewModel(
                application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            pipeline));
        }

        public ActionResult Assembly(string applicationName, string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[applicationName];

            BizTalkAssembly assembly = application.Assemblies[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                     application,
                                      assembly
                                  };

            return View(new AssemblyViewModel(
                application,
                            ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                            manifest,
                            breadCrumbs,
                            
                            installation.Applications.Values,
                            installation.Hosts.Values,
                            assembly));
        }
    }
}