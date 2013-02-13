using System;
using System.Web;
using System.Web.Mvc;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class BizTalkBaseObjectExtension
    {
        /// <summary>
        /// Used to link to the correct path taking the current manifest in 
        /// </summary>
        public static string Path(this BizTalkBaseObject artefact, Manifest manifest)
        {
            if (artefact == null)
                throw new ArgumentNullException("artefact");

            var versionPrefix = string.Empty;
            var reuqestPath = HttpContext.Current.Request.ApplicationPath;

            if (manifest != null && !manifest.IsDefaultLatest)
                versionPrefix = manifest.Version + "/";

            if (reuqestPath != "/")
                reuqestPath = reuqestPath + "/";

            if (artefact is BizTalkApplication)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Id);

            if (artefact is BizTalkAssembly)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/Assembly/", artefact.Id, "/", artefact.Name);

            if (artefact is Orchestration)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/Orchestration/", artefact.Id);

            if (artefact is Pipeline)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/Pipeline/", artefact.Id);

            if (artefact is ReceivePort)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/ReceivePort/", artefact.Id);

            //Here we need to actually go to the receive port that contains the location as location doesn't have their own page
            if (artefact is ReceiveLocation)
            {
                var rp = ((ReceiveLocation) artefact).ReceivePort;
                return string.Concat(reuqestPath, versionPrefix, "Application/", rp.Application.Id, "/ReceivePort/", rp.Id);
            }

            if (artefact is Schema)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/Schema/", artefact.Id, "/", artefact.Name);

            if (artefact is SendPort)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/SendPort/", artefact.Id);

            if (artefact is SendPortGroup)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/SendPortGroup/", artefact.Id);

            if (artefact is Transform)
                return string.Concat(reuqestPath, versionPrefix, "Application/", artefact.Application.Id, "/Map/", artefact.Id);

            if (artefact is Host)
                return string.Concat(reuqestPath, versionPrefix, "Host/", artefact.Id);


            throw new ApplicationException("Unknown BizTalk type");
        }

        public static IHtmlString ShortDescription(this BizTalkBaseObject artefact)
        {
            if (string.IsNullOrEmpty(artefact.Description))
                return new HtmlString(string.Empty);

            var decs = artefact.Description.Length > 100 ? string.Concat(artefact.Description.Substring(0, 100), " ...") : artefact.Description;

            return new HtmlString(string.Concat("<br />", decs));
        }
    }
}