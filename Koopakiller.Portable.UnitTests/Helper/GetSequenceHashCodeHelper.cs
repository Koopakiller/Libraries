using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koopakiller.Portable.UnitTests.Helper
{
    internal class GetSequenceHashCodeHelper
    {
        public static int GetSourceArrayHashCode()
        {
            unchecked
            {
                return (((19 * 31 + 1) * 31 + 1024) * 31 + 1024 * 1024) * 31 + 1024 * 1024 * 1024;
            }
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        public static int[] GetSourceArray()
        {
            return new[] { 1, 1024, 1024 * 1024, 1024 * 1024 * 1024 };
        }
    }
}
