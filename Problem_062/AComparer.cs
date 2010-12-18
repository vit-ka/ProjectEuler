using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem_062
{
    internal class AComparer : IComparer<IList<byte>>, IEqualityComparer<IList<byte>>
    {
        public int Compare(IList<byte> x, IList<byte> y)
        {
            if (x.Count != y.Count)
                return x.Count - y.Count;

            for (int i = 0; i < x.Count; ++i)
                if (x[i] != y[i])
                    return x[i] - y[i];

            return 0;
        }

        public bool Equals(IList<byte> x, IList<byte> y)
        {
            return Compare(x, y) == 0;
        }

        public int GetHashCode(IList<byte> obj)
        {
            return obj.Sum(b => b*11);
        }
    }
}