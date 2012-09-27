using System;
using System.Configuration;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Controllers
{
    //[OutputCache(Duration = int.MaxValue, VaryByParam = "*")]
    public class HomeController : Controller
    {
        public ActionResult Index(string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);
            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);

            return View(new HomeViewModel
            {
                Applications = installation.Applications.Values,
                Manifests = ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                CurrentManifest = manifest
            });
        }
    }
}
