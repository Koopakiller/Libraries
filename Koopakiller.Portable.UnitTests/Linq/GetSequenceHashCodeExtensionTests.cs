using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koopakiller.Linq;
using Koopakiller.Portable.UnitTests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Koopakiller.Portable.UnitTests.Linq
{
    [TestClass]
    public class GetSequenceHashCodeExtensionTests
    {
        [TestMethod]
        public void GetSequenceHashCode()
        {
            var list = GetSequenceHashCodeHelper.GetSourceArray();
            var hash = list.GetSequenceHashCode();
            Assert.AreEqual(GetSequenceHashCodeHelper.GetSourceArrayHashCode(), hash);
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
    }
}
