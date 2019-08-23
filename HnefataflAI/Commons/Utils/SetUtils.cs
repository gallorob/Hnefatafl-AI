using System.Collections.Generic;
using System.Linq;

namespace HnefataflAI.Commons.Utils
{
    public static class SetUtils<T>
    {
        public static void AddRange(HashSet<T> hashSet, T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                hashSet.Add(values[i]);
            }
        }
        public static void AddRange(HashSet<T> hashSet, HashSet<T> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                hashSet.Add(values.ElementAt(i));
            }
        }
        public static void AddIfNotNull(HashSet<T> hashSet, HashSet<T> elems)
        {
            for (int i = 0; i < elems.Count; i++)
            {
                if (elems.ElementAt(i) != null)
                {
                    hashSet.Add(elems.ElementAt(i));
                }
            }
        }
    }
}
