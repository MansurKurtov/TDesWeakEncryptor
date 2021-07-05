using System;

namespace TDesWeakEncryptor
{
    public static class ArrayExtensions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            if (data == null)
                return null;

            if (data.Length < length)
                return null;

            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
