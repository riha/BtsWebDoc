using System.Collections.Generic;
using System.Web.Mvc;
using BizTalkWebDocumenter.Core;
using BizTalkWebDocumenter.Exporter.Model;
using BizTalkWebDocumenter.Model;
using BizTalkWebDocumenter.Web.Extensions;
using BizTalkWebDocumenter.Web.Models;

namespace BizTalkWebDocumenter.Web.Controllers
{
    public class HostController : Controller
    {
        public ActionResult Index(string artifactid, string version)
        {
            var exportReader = new ExportReader();

            BizTalkInstallation installation = exportReader.GetBizTalkInstallation(version);

            Host host = installation.GetHost(artifactid);

            var breadCrumbs = new List<BizTalkBaseObject>
                                  {
                                      host
                                  };

            return View(new HostViewModel
            {
                Installation = installation,
                Host = host,
                BreadCrumbs = breadCrumbs,
                Manifests = exportReader.GetAvailableManifests(version),
                Version = version
            });
        }
    }
}
