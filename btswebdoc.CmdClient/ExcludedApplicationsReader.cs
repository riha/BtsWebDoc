using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace btswebdoc.CmdClient
{
    public static class ExcludedApplicationsReader
    {
        public static HashSet<string> ExcludedApplicationList()
        {
            var hs = new HashSet<string>();

            var excludedApplication = ConfigurationManager.AppSettings["ExcludedApplications"];

            if (excludedApplication != null)
            {
                foreach (var application in  excludedApplication.Split(','))
                {
                    hs.Add(application.Trim());
                }
            }

            return hs;
        }
    }
}
