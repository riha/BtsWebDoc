using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using btswebdoc.Model;

namespace btswebdoc.Web.Extensions
{
    public static class AuthenticationTypeExtension
    {
        public static string Description(this AuthenticationType authenticationType)
        {
            switch (authenticationType)
            {
                case AuthenticationType.NotRequired: return "No authentication";
                case AuthenticationType.RequiredDropMessage: return "Drop messages if authentication fails";
                case AuthenticationType.RequiredKeepMessage: return "Keep messages if authentication fails";
                default: return string.Empty;
            }
        }
    }
}