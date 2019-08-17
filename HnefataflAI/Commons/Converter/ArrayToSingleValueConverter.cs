using HnefataflAI.Commons.Exceptions;
using System;
using System.Reflection;

namespace HnefataflAI.Commons.Converter
{
    public static class ArrayToSingleValueConverter
    {
        /// <summary>
        /// Convert an array of T to a single value of type R
        /// </summary>
        /// <param name="array">The array to convert</param>
        /// <returns>The value R</returns>
        public static int Convert(bool[] array)
        {
            if (array.Length > 32)
            {
                throw new CustomGenericException(typeof(ArrayToSingleValueConverter).Name, MethodBase.GetCurrentMethod().Name, string.Format("Can only convert up to 32 bits, got: {0}", array.Length));
            }
            int result = 0;
            foreach (bool b in array)
            {
                result |= (b ? 1 : 0);
                result <<= 1;
            }
            result >>= 1;
            return result;
        }
    }
}
