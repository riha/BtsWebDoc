using System;
using System.Collections.Generic;
using System.Linq;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class TrackingTypesExtensions
    {
        public static List<string> Descriptions(this TrackingTypes trackingTypes)
        {
            var descriptions = new List<string>();
            var available = Enum.GetValues(typeof(TrackingTypes)).Cast<Enum>();
            
            foreach (TrackingTypes trackingType in available.Where(trackingTypes.HasFlag))
            {
                descriptions.Add(TrackingTypeDescription(trackingType));
            }

            return descriptions;
        }

        private static string TrackingTypeDescription(TrackingTypes trackingType)
        {
            switch (trackingType)
            {
                case TrackingTypes.AfterReceivePipeline: return "Request message bodies after port processing";
                case TrackingTypes.AfterSendPipeline: return "Request message bodies after port processing";
                case TrackingTypes.BeforeReceivePipeline: return "Request message bodies before port processing";
                case TrackingTypes.BeforeSendPipeline: return "Request message bodies before port processing";
                case TrackingTypes.TrackPropertiesAfterReceivePipeline: return "Request message properties after port processing";
                case TrackingTypes.TrackPropertiesAfterSendPipeline: return "Request message properties after port processing";
                case TrackingTypes.TrackPropertiesBeforeReceivePipeline: return "Request message properties before port processing";
                case TrackingTypes.TrackPropertiesBeforeSendPipeline: return "Request message properties before port processing";
                default: return string.Empty;
            }
        }
    }
}