using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGLBase.Extensions
{
    public static class CommonExtensions
    {
        public static List<TValue> ToValueList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic)
        {
            var list = new List<TValue>();
            foreach (var item in dic)
            {
                list.AddRange(item.Value);
            }

            return list;
        }

        public static List<TValue> ToValueList<TKey,TValue>(this Dictionary<TKey, TValue> dic)
        {
            var list = new List<TValue>();

            foreach (var item in dic)
            {
                list.Add(item.Value);
            }

            return list;
        }
    }
}
