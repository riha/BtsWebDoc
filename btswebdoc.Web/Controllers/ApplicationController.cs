using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Controllers
{
    //[OutputCache(Duration = int.MaxValue, VaryByParam = "*")]
    public class ApplicationController : Controller
    {
        public ActionResult Index(string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            BizTalkApplication application = installation.Applications[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject> { application };

            return View(new ApplicationViewModel
                            {
                                Application = application,
                                Applications = installation.Applications.Values,
                                CurrentApplication = application,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new SendPortViewModel
                            {
                                CurrentApplication = application,
                                Applications = installation.Applications.Values,
                                SendPort = sendPort,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new ReceivePortViewModel
                            {
                                CurrentApplication = application,
                                Applications = installation.Applications.Values,
                                ReceivePort = receivePort,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new OrchestrationViewModel
                            {
                                CurrentApplication = application,
                                Applications = installation.Applications.Values,
                                Orchestration = orchestration,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new TransformViewModel
                            {
                                CurrentApplication = application,
                                Applications = installation.Applications.Values,
                                Transform = transform,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest,
                            });
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

            return View(new SchemaViewModel
                            {
                                Applications = installation.Applications.Values,
                                CurrentApplication = application,
                                Schema = schema,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new PipelineViewModel
                            {
                                CurrentApplication = application,
                                Applications = installation.Applications.Values,
                                Pipeline = pipeline,
                                BreadCrumbs = breadCrumbs,
                                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                                CurrentManifest = manifest
                            });
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

            return View(new AssemblyViewModel
            {
                CurrentApplication = application,
                Applications = installation.Applications.Values,
                Assembly = assembly,
                BreadCrumbs = breadCrumbs,
                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                CurrentManifest = manifest
            });
        }
    }
}