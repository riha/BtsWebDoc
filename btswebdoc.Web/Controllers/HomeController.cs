using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string version)
        {
            Manifest manifest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);
            BizTalkInstallation installation = InstallationReader.GetBizTalkInstallation(manifest);
            
            var breadCrumbs = new List<BizTalkBaseObject>();

            return View(new HomeViewModel(
                ManifestReader.GetAllManifests(Request.PhysicalApplicationPath),
                manifest,
                breadCrumbs,
                installation.Applications.Values,
                installation.Hosts.Values));
        }
    }
}
