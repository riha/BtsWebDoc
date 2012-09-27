using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace btswebdoc.Web.Models
{
    public enum ArtifactType
    {
        None,
        Application,
        Pipeline,
        SendPort,
        ReceivePort,
        Schema,
        Assembly,
        Transform,
        Host,
        Orchestration
    }
}