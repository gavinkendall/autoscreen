using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace XnaFan.ImageComparison
{
    /// <summary>
    /// Helperclass for comparing arrays of equal length containing comparable items
    /// </summary>
    /// <typeparam name="T">The type of items to compare - must be IComparable</typeparam>
    class ArrayComparer<T> : IComparer<T[,]> where T : IComparable
    {
        public int Compare(T[,] array1, T[,] array2)
        {
            for (int x = 0; x < array1.GetLength(0); x++)
            {
                for (int y = 0; y < array2.GetLength(1); y++)
                {
                    int comparisonResult = array1[x, y].CompareTo(array2[x, y]);
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }
                }
            }
            return 0;
        }
    }
}