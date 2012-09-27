using System;
using System.Linq;
using System.Xml.Linq;

namespace btswebdoc.Model
{
    public class Manifest
    {
        private DateTime _id;
        public string Server { get; private set; }
        public string Database { get; private set; }

        public const string FileName = "manifest.xml";
        public string Path { get; set; }
        public string Comment { get; private set; }
        public string Environment { get; private set; }
        public bool IsDefaultLatest { get; set; }

        public Manifest(DateTime id, string server, string database, string comment = null, string environment = null)
        {
            _id = id;
            Server = server;
            Database = database;
            Comment = comment;
            Environment = environment;
        }

        public string Name
        {
            get
            {
                if (IsDefaultLatest)
                {
                    return "Latest";
                }

                return _id.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public string Version
        {
            get { return _id.ToString("yyyyMMddHHmmss"); }
        }

        public string Date
        {
            get { return _id.ToString("yyyy-MM-dd HH:mm"); }
        }

        public static Manifest Load(string exportPath)
        {
            var doc = XDocument.Load(exportPath);
            var manifest = (from t in doc.Descendants("Manifest")
                            select new Manifest(DateTime.Parse(t.Attribute("Id").Value),
                                t.Attribute("Server").Value,
                                t.Attribute("Database").Value)
                                       {
                                           Comment = t.Element("Comment") != null ? t.Element("Comment").Value : string.Empty,
                                           Environment = t.Attribute("Environment") != null ? t.Attribute("Environment").Value : string.Empty
                                       }
                                ).Single();

            return manifest;
        }


        public void Save(string exportPath)
        {
            var root = new XElement("Manifest");
            root.Add(new XAttribute("Id", _id));
            root.Add(new XAttribute("Server", Server));
            root.Add(new XAttribute("Database", Database));
            root.Add(new XAttribute("Environment", Environment));

            if (!string.IsNullOrEmpty(Comment))
            {
                root.Add(new XElement("Comment", Comment));
            }

            var document = new XDocument();
            document.Add(root);

            document.Save(exportPath);
        }
    }
}