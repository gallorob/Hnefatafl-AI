using System;
using System.Collections.Generic;

namespace HnefataflAI.Commons.Utils
{
    public class ListUtils
    {
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
    }
}
