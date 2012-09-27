using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using btswebdoc.Shared.Extensions;
using System.Diagnostics;
using Microsoft.BizTalk.ExplorerOM;

namespace btswebdoc.CmdClient
{
    public class OrchestrationOverviewImage
    {
        public string ViewData { get; set; }
        private string ArtifactData { get; set; }
        public List<OrchShape> ShapeMap { get; set; }
        
        private BtsOrchestration _orchestration;
        
        public OrchestrationOverviewImage(BtsOrchestration orchestration)
        {
            _orchestration = orchestration;
            LoadInternalData(orchestration.BtsAssembly.DisplayName);
        }

        public Bitmap GetImage()
        {
            return OrchViewer.GetOrchestationImage(this, _orchestration);
        }       

        private void LoadInternalData(string parentAssemblyName)
        {
            try
            {
                Assembly asm = Assembly.Load(parentAssemblyName);
                Type t = asm.GetType(_orchestration.FullName);

                FieldInfo pi = t.GetField("_symInfo", BindingFlags.NonPublic | BindingFlags.Static);
                object viewData = pi.GetValue(t);
                ViewData = viewData != null ? viewData.ToString() : string.Empty;

                FieldInfo fi = t.GetField("_symODXML", BindingFlags.NonPublic | BindingFlags.Static);
                object artifactData = fi.GetValue(t);
                ArtifactData = artifactData != null ? artifactData.ToString() : string.Empty;
                int pos = ArtifactData.IndexOf("?>");
                if (pos > 0)
                {
                    ArtifactData = ArtifactData.Substring(pos + 2);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.NestedExceptionMessage());
            }
        }
    }
}
