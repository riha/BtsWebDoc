using System;
using System.Collections.Generic;

namespace btswebdoc.Model
{
    [Serializable]
    public class FilterGroup
    {
        public List<Filter> Filters { get; set; }

        public FilterGroup()
        {
            Filters = new List<Filter>();
        }
    }
}