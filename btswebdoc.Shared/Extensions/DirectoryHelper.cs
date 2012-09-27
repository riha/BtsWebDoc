using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace btswebdoc.Shared.Extensions
{
    public static class DirectoryHelper
    {
        public static string CreateDirectory(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath;
        }
    }
}
