using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace btswebdoc.Web.Extensions
{
    public class BinaryFileResult : FilePathResult
    {
        public string Charset { get; set; }
        public string ContentEncoding { get; set; }

        public BinaryFileResult(string fileName, string contentType) : base(fileName, contentType) { }

        protected override void WriteFile(HttpResponseBase response)
        {
            if (this.Charset != null)
                response.Charset = this.Charset;

            if (this.ContentEncoding != null)
                response.AppendHeader("Content-Encoding", this.ContentEncoding);

            base.WriteFile(response);
        }
    }
}