using System;
using System.Text;

namespace btswebdoc.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static string NestedExceptionMessage(this Exception exception)
        {
            if (exception == null)
                return string.Empty;

            var sb = new StringBuilder();

            do
            {
                sb.AppendLine(exception.Message);
                exception = exception.InnerException;
            } while (exception != null);

            return sb.ToString();
        }
    }
}