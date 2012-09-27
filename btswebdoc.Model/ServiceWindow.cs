using System;

namespace btswebdoc.Model
{
        [Serializable]
    public class ServiceWindow
    {
        public DateTime FromTime { get; set; }
        
        public DateTime ToTime { get; set; }

        public bool Enabled { get; set; }
    }
}