using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using btswebdoc.CmdClient.Extensions;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class SchemaModelTransformer
    {
        private Schema _schema;

        internal Schema TransformModel(Microsoft.BizTalk.ExplorerOM.Schema omSchema)
        {
            var schema = new Schema();

            schema.Name = omSchema.FullName;
            schema.TargetNamespace = omSchema.TargetNameSpace;
            schema.RootName = omSchema.RootName ?? string.Empty;

            schema.Description = omSchema.Description;

            if (schema.RootName.Length > 0)
            {
                schema.QualifiedName = String.Format("{0},{1}#{2}", omSchema.AssemblyQualifiedName, omSchema.TargetNameSpace,
                                                  omSchema.RootName);
            }
            else
            {
                schema.QualifiedName = String.Format("{0},{1}", omSchema.AssemblyQualifiedName, omSchema.TargetNameSpace);
            }

            return schema;
        }


        internal void SetReferences(Schema schema, BizTalkArtifacts artifacts, Microsoft.BizTalk.ExplorerOM.Schema omSchema)
        {
            schema.Application = artifacts.Applications[omSchema.Application.Id()];
            schema.ParentAssembly = artifacts.Assemblies[omSchema.BtsAssembly.Id()];

            if (_schema == null || _schema.Name == null || !_schema.Name.Equals(schema.Name, StringComparison.Ordinal))
            {
                var source = new XmlDocument();
                source.LoadXml(omSchema.GetXmlContent());

                var mgr = new XmlNamespaceManager(source.NameTable);
                mgr.AddNamespace("b", "http://schemas.microsoft.com/BizTalk/2003");
                mgr.AddNamespace("x", "http://www.w3.org/2001/XMLSchema");

                SetSchemaType(schema, source, mgr);
                SetSchemaImports(schema, source, mgr, artifacts);

                if (omSchema.Properties != null)
                {
                    foreach (DictionaryEntry property in omSchema.Properties)
                    {
                        schema.Properties.Add(new KeyValuePair<string, string>(property.Key.ToString(),
                                                                               property.Value.ToString()));
                    }
                }

                _schema = schema;
            }
            else
            {
                schema.Properties = _schema.Properties;
                schema.SchemaType = _schema.SchemaType;
                schema.ReferencedSchemas = _schema.ReferencedSchemas;
            }
        }

        private static void SetSchemaImports(Schema schema, XmlDocument schemaDoc, XmlNamespaceManager mgr, BizTalkArtifacts artifacts)
        {
            XmlNodeList importedSchemaNodes = schemaDoc.SelectNodes("//x:appinfo/b:imports/b:namespace", mgr);
            foreach (XmlNode importedSchemaNode in importedSchemaNodes)
            {
                var refSchema = new Schema
                {
                    Name = importedSchemaNode.Attributes.GetNamedItem("location").Value,
                    TargetNamespace = importedSchemaNode.Attributes.GetNamedItem("uri").Value
                };

                Schema s;
                if (artifacts.Schemas.TryGetValue(refSchema.Id, out s))
                    schema.ReferencedSchemas.Add(s);

                //string importedPrefix = importedSchemaNode.Attributes.GetNamedItem("prefix").Value;

                // Select properties used by this schema that are declared in the property schema
                //XmlNodeList importedPropertyNodes = schemaDoc.SelectNodes("//x:appinfo/b:properties/b:property", mgr);
                //foreach (XmlNode importedPropertyNode in importedPropertyNodes)
                //{
                //    XmlAttribute nameAttribute = importedPropertyNode.Attributes.GetNamedItem("name") as XmlAttribute;

                //    if (nameAttribute == null)
                //    {
                //        nameAttribute = importedPropertyNode.Attributes.GetNamedItem("distinguished") as XmlAttribute;
                //    }

                //    if (nameAttribute != null)
                //    {
                //        string propertyName = nameAttribute.Value;

                //        if (propertyName.StartsWith(importedPrefix))
                //        {
                //            string[] nameParts = propertyName.Split(new char[] { ':' });

                //            if (nameParts.Length > 0)
                //            {
                //                propertyName = nameParts[1];
                //                // importedSchemaObj.Properties.Add(propertyName);
                //            }
                //        }
                //    }
                //}
            }
        }

        private void SetSchemaType(Schema schema, XmlDocument schemaDoc, XmlNamespaceManager mgr)
        {
            XmlNode schemaInfo = schemaDoc.SelectSingleNode("//x:appinfo/b:schemaInfo", mgr);

            if (schemaInfo != null && schemaInfo.Attributes != null)
            {
                var envelope = schemaInfo.Attributes.GetNamedItem("is_envelope") as XmlAttribute;
                var schmeaType = schemaInfo.Attributes.GetNamedItem("schema_type") as XmlAttribute;

                if (envelope != null && envelope.Value == "yes")
                {
                    schema.SchemaType = SchemaType.Envelope;
                }
                else if (schmeaType != null && schmeaType.Value == "property")
                {
                    schema.SchemaType = SchemaType.Property;
                }
                else
                {
                    schema.SchemaType = SchemaType.Document;
                }
            }
        }

    }
}
