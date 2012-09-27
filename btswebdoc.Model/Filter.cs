using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class Filter
    {
        public string Property { get; set; }

        public string Value { get; set; }

        public string FilterOperator { get; set; }
    }
}