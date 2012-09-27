namespace btswebdoc.Web.Extensions
{
    public static class StringExtensions
    {
        public static string Short(this string text)
        {
            return string.Concat(text.Substring(0, 20), " ...");
        }
    }
}