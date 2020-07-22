using System;
using UnityEngine;

namespace Andification.Runtime.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] ToOneDimensional<T>(this T[,] arr)
        {
            if (arr == null)
                throw new ArgumentNullException("Array was null");

            int h = arr.GetLength(0);
            int w = arr.GetLength(1);
            T[] outArr = new T[w * h];

            for (int _h = 0; _h < h; _h++)
                for (int _w = 0; _w < w; _w++)
                    outArr[_w + (_h * w)] = arr[_h, _w];

            return outArr;
        }

        public static T[,] ToTwoDimensional<T>(this T[] arr, int x, int y)
        {
            if (arr == null)
                throw new NullReferenceException("Array was null");

            if (x < 0 || y < 0)
                throw new ArgumentOutOfRangeException("x and y must be a positive integer");

            if (x * y < arr.Length)
                throw new ArgumentOutOfRangeException($"The contents of the one dimensional array (Size: {arr.Length}) does not fit within the new size of the array (Size: {x * y})");

            T[,] twoDimArr = new T[x, y];

            for (int i = 0, xIndex = 0, yIndex = 0; i < arr.Length; )
            {
                if (yIndex >= y)
                {
                    xIndex++;
                    yIndex = 0;
                }

                //Debug.Log($"twoDimArr[xIndex, yIndex++] = i < arr.Length ? arr[i++] : default;\ntwoDimArr[{xIndex}, {yIndex++}] = {i} < {arr.Length} ? arr[{i++}] : default;");
                twoDimArr[xIndex, yIndex++] = i < arr.Length ? arr[i++] : default;
            }

            return twoDimArr;
        }

        public static T[] SubArray<T>(this T[] arr, int start, int end = -1)
        {
            if (arr == null)
                throw new ArgumentNullException("Array was null");

            if (start < 0)
                throw new IndexOutOfRangeException("Start index cannot be smaller than 0");

            if (start > arr.Length - 1)
                throw new IndexOutOfRangeException($"Start index ({start}) exceeds array max index of {arr.Length - 1}");

            if (end > arr.Length - 1)
                throw new IndexOutOfRangeException($"End index ({end}) exceeds array max index of {arr.Length - 1}");

            if (end < start)
                throw new Exception($"End index ({end}) cannot be smaller than or equal to start index ({start})");

            var splitArrSize = (end < 0 ? arr.Length - 1 : end) - start;
            var splitArr = new T[splitArrSize + 1];
            for (int i = start, s = 0; i < (end < 0 ? arr.Length : end + 1); i++, s++)
                splitArr[s] = arr[i];

            return splitArr;
        }
    }
}