using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaFan.ImageComparison
{
    /// <summary>
    /// Helperclass which compares tuples with imagepath and grayscale based on the values in their grayscale array
    /// </summary>
    class PathGrayscaleTupleComparer : IComparer<Tuple<string, byte[,]>>
    {
        private static ArrayComparer<byte> _comparer = new ArrayComparer<byte>();
        public int Compare(Tuple<string, byte[,]> x, Tuple<string, byte[,]> y)
        {
            return _comparer.Compare(x.Item2, y.Item2);
        }
    }
}
