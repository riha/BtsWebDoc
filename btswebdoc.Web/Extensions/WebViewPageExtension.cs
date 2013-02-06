using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using btswebdoc.Web.Models;

namespace btswebdoc.Web.Extensions
{
    public static class WebViewPageExtension
    {

        /// <summary>
        /// Adds values from the model to the view bag to make the accessable in the template pages
        /// </summary>
        public static void AddToViewBag(this WebViewPage page, ViewModelBase model)
        {
            page.ViewBag.Applications = model.Applications;
            page.ViewBag.Manifests = model.Manifests;
            page.ViewBag.BreadCrumbs = model.BreadCrumbs;
            page.ViewBag.Hosts = model.Hosts;
        }
    }
}