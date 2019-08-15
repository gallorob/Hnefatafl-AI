using System;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Utils
{
    public class ListUtils
    {
        /// <summary>
        /// Shuffle a list randomly
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="list">The list to shuffle</param>
        public static void ShuffleList<T>(IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n));
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        /// <summary>
        /// Add a object to a list only if it isn't null
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to add</param>
        /// <param name="list">The list to add the object to</param>
        public static void AddIfNotNull<T>(T obj, List<T> list)
        {
            if (obj != null)
            {
                list.Add(obj);
            }
        }
        public static void AddIfNotNull<T>(List<T> objs, List<T> list)
        {
            foreach (T obj in objs)
            {
                if (obj != null)
                {
                    list.Add(obj);
                }
            }
        }
    }
}
