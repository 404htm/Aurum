using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TV SafeGet<TK, TV>(this Dictionary<TK, TV> dictionary, TK key)
        {
            TV value;
            return dictionary.TryGetValue(key, out value) ? value : default(TV);
        }
    }
}
