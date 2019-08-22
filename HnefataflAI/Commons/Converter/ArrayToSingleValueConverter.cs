using HnefataflAI.Commons.Exceptions;
using System;
using System.Reflection;

namespace HnefataflAI.Commons.Converter
{
    public static class ArrayToSingleValueConverter
    {
        /// <summary>
        /// Convert an array of bool to a single int
        /// </summary>
        /// <param name="array">The array to convert</param>
        /// <returns>The converted value</returns>
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
        /// <summary>
        /// Convert two bool array to a single int
        /// </summary>
        /// <param name="array1">The first array to convert</param>
        /// <param name="array2">The second array to convert</param>
        /// <returns>The converted value</returns>
        public static int Convert(bool[] array1, bool[] array2)
        {
            if (array1.Length + array2.Length > 32)
            {
                throw new CustomGenericException(typeof(ArrayToSingleValueConverter).Name, MethodBase.GetCurrentMethod().Name, string.Format("Can only convert up to 32 bits, got: {0}", array1.Length + array2.Length));
            }
            return (Convert(array1) << array2.Length) | Convert(array2);
        }
    }
}
