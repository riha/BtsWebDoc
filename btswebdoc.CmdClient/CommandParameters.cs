using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace btswebdoc.CmdClient
{
    /// <summary>
    /// Class to hold parameters from the commands line. 
    /// </summary>
    public class CommandParameters
    {
        public bool ShouldShowHelp;
        public bool ShouldExport;
        public string Database = "BizTalkMgmtDb";
        public string Server = ".";
        public string Comment;
        public string Folder;
        public string Environment;
        public readonly HashSet<string> ExcludedApplications = new HashSet<string>();

        public void SetExcludedApplications(string list)
        {
            if (!string.IsNullOrEmpty(list))
            {
                foreach (var application in list.Split(','))
                {
                    ExcludedApplications.Add(application.Trim());
                }
            }
        }
    }
}
