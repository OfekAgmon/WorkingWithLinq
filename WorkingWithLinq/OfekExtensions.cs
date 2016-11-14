using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithLinq
{
    public static class OfekExtensions
    {
        public static string addA(this string text)
        {
            return text + "A";
        }
         
        public static string addB(this string text)
        {
            return text + "B";
        }

        public static string addC(this string text)
        {
            return text + "C";
        }

        public static IEnumerable<T> ofekFilter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var result = new List<T>();

            foreach(var  item in source)
            {
                if (predicate(item)) result.Add(item);
            }

            return result;
        }

        public static IEnumerable<T> ofekBetterFilter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item)) yield return item;
            }
        }
    }
}
