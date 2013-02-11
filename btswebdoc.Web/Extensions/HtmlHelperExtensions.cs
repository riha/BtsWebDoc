using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Shared;

namespace btswebdoc.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        private static string _dividerHtml = "<span class=\"divider\">/</span>";

        public static string Version(this HtmlHelper html)
        {
            var info = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;
            return info == null ? "N/A" : info.InformationalVersion;
        }

        public static IHtmlString Breadcrumbs(this HtmlHelper html, IEnumerable<BizTalkBaseObject> breadcrumbs, Manifest manifest)
        {
            var builder = new StringBuilder();
            builder.Append("<ul class=\"breadcrumb\">");
            builder.Append(GetBreadcrumbHomePath((breadcrumbs == null || breadcrumbs.Count() < 1), manifest));

            if (breadcrumbs != null)
            {
                for (int i = 0; i < breadcrumbs.Count(); i++)
                {
                    var artefact = breadcrumbs.ElementAt(i);

                    if (i != breadcrumbs.Count() - 1)
                    {
                        builder.Append(BuildAnchor(artefact.Path(manifest), GetArtifactPrefix(artefact) + artefact.Name, new List<string> { "force-wrap" }));
                        builder.Append(_dividerHtml);
                    }
                    else
                    {
                        builder.Append(GetArtifactPrefix(artefact));
                        builder.AppendFormat("<span class='force-wrap'>{0}</span>", artefact.Name);
                    }
                }
            }
            builder.Append("</ul>");
            return MvcHtmlString.Create(builder.ToString());
        }

        public static string GetVersionedHomePath(this HtmlHelper html, Manifest manifest)
        {
            var reuqestPath = HttpContext.Current.Request.ApplicationPath;

            if (reuqestPath != "/")
                reuqestPath = reuqestPath + "/";

            if (manifest != null && !manifest.IsDefaultLatest)
                reuqestPath = reuqestPath + manifest.Version + "/";

            return reuqestPath;
        }

        private static string GetBreadcrumbHomePath(bool isLast, Manifest manifest)
        {
            if (isLast)
                return "Home";

            var builder = new StringBuilder();

            if (manifest != null)
                builder.Append(BuildAnchor(string.Format("/{0}/", manifest), "Home"));
            else
                builder.Append(BuildAnchor("/", "Home"));

            builder.Append(_dividerHtml);
            return builder.ToString();
        }

        private static string BuildAnchor(string href, string text, List<string> cssClasses = null)
        {
            var tb = new TagBuilder("a");

            if (cssClasses != null)
                tb.Attributes["class"] = string.Join(",", cssClasses.ToArray());

            tb.Attributes["href"] = href;

            tb.SetInnerText(text);

            return tb.ToString();
        }

        private static string GetArtifactPrefix(BizTalkBaseObject artefact)
        {
            if (artefact is BizTalkApplication)
                return "Application: ";

            if (artefact is Pipeline)
                return "Pipeline: ";

            if (artefact is SendPort)
                return "Send port: ";

            if (artefact is ReceivePort)
                return "Receive port: ";

            if (artefact is Schema)
                return "Schema: ";

            if (artefact is BizTalkAssembly)
                return "Assembly: ";

            if (artefact is Transform)
                return "Map: ";

            if (artefact is Host)
                return "Host: ";

            if (artefact is Orchestration)
                return "Orchestration: ";

            return string.Empty;
        }

        public static string PageTitle(this HtmlHelper html, BizTalkBaseObject artefact)
        {
            const string title = Constants.ApplicationName;

            if (artefact is BizTalkApplication)
                return string.Concat(title, " - Application: ", artefact.Name);

            if (artefact is Pipeline)
                return string.Concat(title, " - Pipeline: ", artefact.Name);

            if (artefact is SendPort)
                return string.Concat(title, " - Send port: ", artefact.Name);

            if (artefact is ReceivePort)
                return string.Concat(title, " - Receive port: ", artefact.Name);

            if (artefact is Schema)
                return string.Concat(title, " - Schema: ", artefact.Name);

            if (artefact is BizTalkAssembly)
                return string.Concat(title, " - Assembly: ", artefact.Name);

            if (artefact is Transform)
                return string.Concat(title, " - Map: ", artefact.Name);

            if (artefact is Host)
                return string.Concat(title, " - Host: ", artefact.Name);

            if (artefact is Orchestration)
                return string.Concat(title, " - Orchestration: ", artefact.Name);

            return title;
        }

    }
}