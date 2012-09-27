using System;

namespace btswebdoc.Model
{
    [Serializable]
    public class OrchestrationPort
    {
        public string Name { get; set; }

        public SendPort SendPort { get; set; }

        public ReceivePort ReceivePort { get; set; }
    }
}