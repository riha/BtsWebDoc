using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using btswebdoc.Shared.Extensions;
using Microsoft.BizTalk.ExplorerOM;

namespace btswebdoc.CmdClient.Extensions
{
    static class ExplorerOmModelExtension
    {
        public static string Id(this Schema omSchema)
        {
            return omSchema.QualifiedName().GetCheckSum(); 
        }

        public static string QualifiedName(this Schema omSchema)
        {
            return string.Concat(omSchema.FullName, omSchema.RootName, omSchema.TargetNameSpace);
        }

        public static string Id(this Pipeline omPipeline)
        {
            return omPipeline.FullName;
        }

        public static string Id(this BtsAssembly omAssembly)
        {
            return omAssembly.DisplayName;
        }

        public static string Id(this Application omApplication)
        {
            return omApplication.Name;
        }

        public static string Id(this ReceivePort omReceivePort)
        {
            return omReceivePort.Name;
        }

        public static string Id(this SendPort omSendPort)
        {
            return omSendPort.Name;
        }

        public static string Id(this Transform omTransform)
        {
            return omTransform.FullName;
        }

        public static string Id(this BtsOrchestration omOrchestration)
        {
            return omOrchestration.FullName;
        }

        private static FieldInfo _xmlContentField = null;

        public static string GetXmlContent(this Schema schema)
        {
            if (_xmlContentField == null)
            {
                // The XmlContent property of Schema caches the retrieved content indefinetely. Due to the huge amount of data stored in XmlContent 
                // which can never be released we have to resort to this hack of locating the backing store and clearing it after each schema export.
                var type = typeof (Schema);
                var field = type.GetField("xmlContent", BindingFlags.Instance | BindingFlags.NonPublic);

                if (field == null)
                    throw new Exception("Expected private field named xmlContent on Schema class. Assembly has changed.");

                _xmlContentField = field;
            }

            var content = schema.XmlContent;
            _xmlContentField.SetValue(schema, null);

            return content;
        }

        public static string GetXmlContent(this Transform transform)
        {
            var content = transform.XmlContent;

            // The XmlContent property of Schema caches the retrieved content indefinetely. Due to the huge amount of data stored in XmlContent 
            // which can never be released we have to resort to this hack of locating the backing store and clearing it after each schema export.
            var type = typeof(Transform);
            var field = type.GetField("xmlContent", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field == null)
                throw new Exception("Expected private field named xmlContent on Schema class. Assembly has changed.");

            field.SetValue(transform, null);

            return content;
        }
    }
}
