using System.IO;
using System.Text;
using System.Web.Mvc;
using btswebdoc.Model;
using btswebdoc.Web.DocsReaders;
using btswebdoc.Web.Extensions;
using System.Xml;
using System.IO.Compression;

namespace btswebdoc.Web.Controllers
{
    //[OutputCache(Duration = int.MaxValue, VaryByParam = "*")]
    public class AssetsController : Controller
    {
        public ActionResult Favicon()
        {
            return new HttpNotFoundResult();
        }

        public BinaryFileResult Map(string applicationName, string artifactid, string version)
        {
            Manifest mainfest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            string filePath = Path.Combine(Path.Combine(mainfest.Path, "Assets/Maps"), string.Concat(artifactid, ".gz"));

            Encoding encoding = GetEncoding(filePath);

            return new BinaryFileResult(filePath, "text/xml")
            {
                Charset = encoding.HeaderName,
                ContentEncoding = "gzip"
            };
        }

        private static Encoding GetEncoding(string filePath)
        {
            Encoding encoding;

            using (var fs = System.IO.File.OpenRead(filePath))
            using (var gz = new GZipStream(fs, CompressionMode.Decompress))
            using (var reader = new XmlTextReader(gz))
            {
                reader.MoveToContent();
                encoding = reader.Encoding;
            }

            return encoding;
        }

        public BinaryFileResult Schema(string artifactid, string version)
        {
            Manifest mainfest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            string filePath = Path.Combine(Path.Combine(mainfest.Path, "Assets/Schemas"), string.Concat(artifactid, ".gz"));

            Encoding encoding = GetEncoding(filePath);

            return new BinaryFileResult(filePath, "text/xml")
            {
                Charset = encoding.HeaderName,
                ContentEncoding = "gzip"
            };
        }

        public ActionResult Orchestration(string artifactid, string version)
        {
            Manifest mainfest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            return File(Path.Combine(Path.Combine(mainfest.Path, "Assets/Overview"), string.Concat(artifactid, ".jpg")), "image/jpeg");
        }

        public ActionResult OrchestrationThumb(string artifactid, string version)
        {
            Manifest mainfest = ManifestReader.GetCurrentManifest(version, Request.PhysicalApplicationPath);

            return File(Path.Combine(Path.Combine(mainfest.Path, "Assets/Overview"), string.Concat(artifactid, "_thumb.jpg")), "image/jpeg");
        }
    }
}