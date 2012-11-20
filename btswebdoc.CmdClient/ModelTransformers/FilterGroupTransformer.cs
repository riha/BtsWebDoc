using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using btswebdoc.Model;
using System.Xml;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class FilterGroupTransformer
    {
        internal static List<FilterGroup> CreateFilterGroups(string filterXml)
        {
            var groups = new List<FilterGroup>();

            if (!string.IsNullOrEmpty(filterXml))
            {
                var doc = new XmlDocument();
                doc.LoadXml(filterXml);

                foreach (XmlNode groupNode in doc.SelectNodes("./Filter/Group"))
                {
                    var group = new FilterGroup();
                    foreach (XmlNode statementNode in groupNode.SelectNodes("./Statement"))
                    {
                        var filter = new Filter
                        {
                            Property = statementNode.Attributes.GetNamedItem("Property").Value,
                            FilterOperator = statementNode.Attributes.GetNamedItem("Operator").Value
                        };

                        XmlNode valueNode = statementNode.Attributes.GetNamedItem("Value");

                        if (valueNode != null)
                        {
                            filter.Value = valueNode.Value;
                        }

                        group.Filters.Add(filter);
                    }

                    groups.Add(group);
                }
            }

            return groups;
        }
    }
}
