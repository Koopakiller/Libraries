using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koopakiller.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Koopakiller.Portable.UnitTests.Linq
{
    [TestClass]
    public class GetSequenceHashCodeExtensionTests
    {
        [TestMethod]
        public void GetSequenceHashCode()
        {
            var list = GetSequenceHashCodeExtensionTests.GetSourceArray();
            var hash = list.GetSequenceHashCode();
            Assert.AreEqual(GetSequenceHashCodeExtensionTests.GetSourceArrayHashCode(), hash);
        }

        [TestMethod]
        public void GetSequenceHashCode_EmptySource()
        {
            var list = new int[0];
            var hash = list.GetSequenceHashCode();
            Assert.AreEqual(19, hash);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSequenceHashCode_NullSource()
        {
            var list = (int[])null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var hash = list.GetSequenceHashCode();
        }

        #region Helper

        private static int GetSourceArrayHashCode()
        {
            unchecked
            {
                return (((19 * 31 + 1) * 31 + 1024) * 31 + 1024 * 1024) * 31 + 1024 * 1024 * 1024;
            }
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private static int[] GetSourceArray()
        {
            return new[] { 1, 1024, 1024 * 1024, 1024 * 1024 * 1024 };
        }

        #endregion
    }
}
