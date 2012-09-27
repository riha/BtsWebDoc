using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace btswebdoc.Shared.Extensions
{
    public static class StringExtension
    {
        public static string GetCheckSum(this string value)
        {
            byte[] input = Encoding.UTF8.GetBytes(value);
            byte[] hash = MD5.Create().ComputeHash(input);
            
            var output = new StringBuilder(hash.Length);
            for (int i = 0; i < hash.Length; i++)
            {
                output.Append(hash[i].ToString("X2"));
            }

            return output.ToString();
        }

        public static string GetUrlSafe(this string s)
        {
            return Regex.Replace(s, @"\/", "-slash-");
        }
    }
}
