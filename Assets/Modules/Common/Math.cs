using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Extensions{
    public static class Math
    {
        public static int GetMedianIndex<T>(T[] sourceArray, IComparer<T> comparer, out bool isEven, bool cloneArray = true)
        {
            if (sourceArray == null || sourceArray.Length == 0)
                throw new ArgumentException("Median of empty array not defined.");

            T[] sortedArray = cloneArray ? (T[])sourceArray.Clone() : sourceArray;
            Array.Sort(sortedArray, sortedArray, comparer);

            int size = sortedArray.Length;
            int mid = size / 2;
            if (size % 2 != 0)
            {
                isEven = false;
                return mid;
            }

            isEven = true;
            return mid;
        }
    }
}
