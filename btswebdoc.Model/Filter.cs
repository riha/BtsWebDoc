using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class Filter
    {
        public string Property { get; set; }

        public string Value { get; set; }

        public string FilterOperator { get; set; }

        public static string FilterOperatorToSign(string filterOperator)
        {
            if (filterOperator == "0")
                return "==";

            if (filterOperator == "1")
                return "<";


            if (filterOperator == "2")
                return "<=";

            if (filterOperator == "3")
                return ">";

            if (filterOperator == "4")
                return ">=";

            if (filterOperator == "5")
                return "!=";

            return "Exists";
        }
    }
}