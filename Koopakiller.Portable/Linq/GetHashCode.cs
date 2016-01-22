using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koopakiller.Linq
{
    public static class GetSequenceHashCodeExtension
    {
        public static int GetSequenceHashCode<T>(this IEnumerable<T> source)
        {
            unchecked
            {
                return source.Aggregate(19, (current, foo) => current * 31 + foo.GetHashCode());
            }
        }
    }
}
