using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using btswebdoc.Model;

namespace btswebdoc.CmdClient.ModelTransformers
{
    class TrackingTypeModelTransformer
    {
        internal static TrackingTypes TransformModel(Microsoft.BizTalk.ExplorerOM.TrackingTypes omTrackingTypes)
        {
            TrackingTypes t = (TrackingTypes) omTrackingTypes;
            //var availableOmTrackingTypes = Enum.GetValues(typeof(Microsoft.BizTalk.ExplorerOM.TrackingTypes)).Cast<Enum>();
            //t = availableOmTrackingTypes.Where(omTrackingTypes.HasFlag).Cast<TrackingTypes>();
            //foreach (Microsoft.BizTalk.ExplorerOM.TrackingTypes omTrackingType in )
            //{
            //  t = 
            //}
        }
    }
}
