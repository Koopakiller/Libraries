using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Koopakiller.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
// ReSharper disable ObjectCreationAsStatement

namespace Koopakiller.Portable.UnitTests.Linq
{
    [TestClass]
    public class RingEnumeratorTests
    {
        #region .ctor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_EmptyEnumerator()
        {
            var enu = null as IEnumerator<int>;
            // ReSharper disable once ExpressionIsAlwaysNull
            using (new RingEnumerator<int>(enu)) { }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_EmptyEnumerable()
        {
            var enu = null as IEnumerable<int>;
            // ReSharper disable once ExpressionIsAlwaysNull
            using (new RingEnumerator<int>(enu)) { }
        }

        #endregion

        #region EnumerateTest

        [TestMethod]
        public void EnumerateTest()
        {
            var arr = new[] { 1, 2, 3 };
            using (var re = new RingEnumerator<int>(arr))
            {
                Assert.IsTrue(re.Take(9).SequenceEqual(new[] { 1, 2, 3, 1, 2, 3, 1, 2, 3 }));
            }
        }

        [TestMethod]
        public void EnumerateTest_EmptySource()
        {
            var arr = new int[] { };
            using (var re = new RingEnumerator<int>(arr))
            {
                Assert.IsFalse(re.Any());
            }
        }

        [TestMethod]
        public void EnumerateTest_WithCache()
        {
            var arr = new[] { 1, 2, 3 };
            using (var re = new RingEnumerator<int>(arr) { UseCache = true })
            {
                Assert.IsTrue(re.Take(9).SequenceEqual(new[] { 1, 2, 3, 1, 2, 3, 1, 2, 3 }));
            }
        }

        [TestMethod]
        public void EnumerateTest_EmptySource_WithCache()
        {
            var arr = new int[] { };
            using (var re = new RingEnumerator<int>(arr) { UseCache = true })
            {
                Assert.IsFalse(re.Any());
            }
        }

        #endregion

        #region UseCache

        [TestMethod]
        public void UseCache_True()
        {
            using (var re = new RingEnumerator<int>(new StrangeTestEnumerator()) { UseCache = true })
            {
                Assert.IsTrue(re.Take(9).SequenceEqual(new[] { 1, 2, 3, 1, 2, 3, 1, 2, 3 }));
            }
        }

        [TestMethod]
        public void UseCache_False()
        {
            using (var re = new RingEnumerator<int>(new StrangeTestEnumerator()))
            {
                Assert.IsTrue(re.Take(9).SequenceEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UseCache_DuringEnumeration()
        {
            var arr = new[] { 1, 2, 3 };
            using (var re = new RingEnumerator<int>(arr))
            {
                re.MoveNext();
                re.UseCache = true;
            }
        }

        #endregion

        #region Reset

        [TestMethod]
        public void Reset()
        {
            using (var re = new RingEnumerator<int>(new StrangeTestEnumerator()))
            {
                for (var i = 0; i < 5; ++i)
                {
                    re.MoveNext();
                }
                Assert.AreEqual(5, re.Current);
                re.Reset();
                re.MoveNext();
                Assert.AreEqual(7, re.Current);//third enumeration, -> 3rd sub-array
            }
        }

        [TestMethod]
        public void Reset_WithCache()
        {
            using (var re = new RingEnumerator<int>(new StrangeTestEnumerator()) { UseCache = true })
            {
                for (var i = 0; i < 5; ++i)
                {
                    re.MoveNext();
                }
                Assert.AreEqual(2, re.Current);
                re.Reset();
                re.MoveNext();
                Assert.AreEqual(4, re.Current);
                //Reset resets the cache too, it will be rebuild and the second sub-array will be cached
            }
        }

        #endregion

        #region IEnumerator Current

        [TestMethod]
        public void IEnumerator_Current()
        {
            var arr = new[] { 1, 2, 3 };
            using (var re = new RingEnumerator<int>(arr))
            {
                var enu = (IEnumerator)re;
                var i = 0;
                while (re.MoveNext())
                {
                    Assert.AreEqual(re.Current, enu.Current);
                    ++i;
                    if (i >= 10)
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        #region IEnumerable GetEnumerator
        
        [TestMethod]
        public void IEnumerable_GetEnumerator()
        {
            var arr = new[] { 1, 2, 3 };
            using (var re = new RingEnumerator<int>(arr))
            {
                var i = 0;
                var enu = ((IEnumerable)re).GetEnumerator();
                while (enu.MoveNext())
                {
                    Assert.AreEqual(re.Current, enu.Current);
                    ++i;
                    if (i >= 10)
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Represents an enumerator for an int-array which return 1,2,3 for the first 
        /// enumeration and 4,5,6 after the first call of <see cref="Reset"/> and 7,8,9 
        /// after the second call. Then it starts again with 1,2,3 and so on.
        /// </summary>
        private class StrangeTestEnumerator : IEnumerator<int>
        {
            private int _x;
            private int _i = -1;
            private readonly int[,] _arr =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };


            public void Dispose() { }

            public bool MoveNext()
            {
                ++this._i;
                return this._i < this._arr.GetLength(0);
            }

            public void Reset()
            {
                this._i = 0 - 1;
                ++this._x;
                if (this._x >= this._arr.GetLength(1))
                {
                    this._x = 0;
                }
            }

            public int Current => this._arr[this._x, this._i];

            object IEnumerator.Current => this.Current;
        }
    }
}
