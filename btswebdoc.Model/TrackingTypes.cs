using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace btswebdoc.Model
{
    [Flags]
    public enum TrackingTypes
    {
        BeforeReceivePipeline = 1,
        AfterReceivePipeline = 2,
        BeforeSendPipeline = 4,
        AfterSendPipeline = 8,
        TrackPropertiesBeforeReceivePipeline = 16,
        TrackPropertiesAfterReceivePipeline = 32,
        TrackPropertiesBeforeSendPipeline = 64,
        TrackPropertiesAfterSendPipeline = 128,
    }
}
