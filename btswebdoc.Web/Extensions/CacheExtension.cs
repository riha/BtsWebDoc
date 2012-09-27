using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace btswebdoc.Web.Extensions
{
    public static class CacheExtension
    {
        public static void Clear(this Cache cache)
        {
            var enumerator = cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                cache.Remove(enumerator.Key.ToString());
            }
        }
    }
}