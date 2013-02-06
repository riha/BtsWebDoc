using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class TransportInfo
    {
        public string Address { get; set; }

        public string Type { get; set; }

        public bool OrderedDelivery { get; set; }

        public int RetryCount { get; set; }

        public int RetryInterval { get; set; }

        public ServiceWindow ServiceWindow { get; set; }

        public Host Host { get; set; }
    }
}