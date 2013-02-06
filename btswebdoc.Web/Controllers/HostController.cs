using System.Collections.Generic;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Controllers
{
    public class HostController : Controller
    {
        public ActionResult Index(string artifactid, string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            Host host = installation.Hosts[artifactid];

            var breadCrumbs = new List<BizTalkBaseObject> { host };

            return View(new HostViewModel(
                host,
                ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                manifest,
                breadCrumbs,

                installation.Applications.Values,
                installation.Hosts.Values,
                host));
        }
    }
}
