using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class HostTypesExtensions
    {
        public static string HostTypeDescription(this HostType hostType)
        {
            switch (hostType)
            {
                case HostType.InProcess: return "In-Process";
                case HostType.Isolated: return "Insolated";
                default: return string.Empty;
            }
        }
    }
}