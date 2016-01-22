using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Koopakiller.Coding.Morse;
using Koopakiller.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ObjectCreationAsStatement
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'

namespace Koopakiller.Portable.UnitTests.Coding.Morse
{
    [TestClass]
    public class MorseWordTests
    {
        #region .ctor

        [TestMethod]
        public void Constructor()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            var mw = new MorseWord(chars);
            Assert.IsTrue(mw.Characters.SequenceEqual(chars));
        }

        [TestMethod]
        public void Constructor_NullArray()
        {
            var mw = new MorseWord(null);
            Assert.AreEqual(0, mw.Characters.Count);
        }

        #endregion

        #region IEnumerable

        [TestMethod]
        public void IEnumerable_GetEnumerator()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            IEnumerable mw = new MorseWord(chars);
            Assert.IsTrue(mw.OfType<MorseCharacter>().SequenceEqual(chars));
        }

        [TestMethod]
        public void IEnumerable_MorseCharacter_GetEnumerator()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            IEnumerable<MorseCharacter> mw = new MorseWord(chars);
            Assert.IsTrue(mw.SequenceEqual(chars));
        }

        [TestMethod]
        public void IEnumerable_Char_GetEnumerator()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            IEnumerable<char> mw = new MorseWord(chars);
            Assert.IsTrue(mw.SequenceEqual(new[] { 'A', 'M', 'Y', '5' }));
        }

        #endregion

        #region ToString

        [TestMethod]
        public void ToString_NoParameter()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("·− −− −·−− ·····", mw.ToString());
        }

        [TestMethod]
        public void ToString_EmptyStringFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("·− −− −·−− ·····", mw.ToString(""));
        }

        [TestMethod]
        public void ToString_mFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("·− −− −·−− ·····", mw.ToString("m"));
        }

        [TestMethod]
        public void ToString_dFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("·− −− −·−− ····· (AMY5)", mw.ToString("d"));
        }

        [TestMethod]
        public void ToString_cFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("AMY5", mw.ToString("c"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_NullFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("C", mw.ToString(null));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToString_InvalidFormat()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            Assert.AreEqual("C", mw.ToString("INVALID"));
        }

        #region CustomDitDah

        [TestMethod]
        public void ToString_CustomDitDah()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            var r = mw.ToString("A", "B", "C");
            Assert.AreEqual("ABCBBCBABBCAAAAA", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullDit()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            var r = mw.ToString(null, "", "");
            Assert.AreEqual("", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullDah()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            var r = mw.ToString("", null, "");
            Assert.AreEqual("", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullCharacterSeparator()
        {
            var mw = new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5);
            var r = mw.ToString("", "", null);
            Assert.AreEqual("", r);
        }

        #endregion

        #endregion

        #region Parse

        [TestMethod]
        public void Parse()
        {
            var mw = MorseWord.Parse("AMY5");
            Assert.AreEqual(new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5), mw);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_WithWhitespace()
        {
            MorseWord.Parse("A B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_IllegalCharacter()
        {
            MorseWord.Parse("~~~");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Null()
        {
            var mw = MorseWord.Parse(null);
            Assert.AreEqual(new MorseWord(MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5), mw);
        }

        [TestMethod]
        public void Parse_Empty()
        {
            var mw = MorseWord.Parse("");
            Assert.AreEqual(new MorseWord(), mw);
        }

        #endregion

        #region GetHashCode / Equals

        [TestMethod]
        public void GetHashCodeTest()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            var mw = new MorseWord(chars);
            Assert.AreEqual(chars.GetSequenceHashCode(), mw.GetHashCode());
        }

        [TestMethod]
        public void EqualsTestNullParameter()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            var mw1 = new MorseWord(chars);
            var mw2 = new MorseWord(chars);
            Assert.IsTrue(mw1.Equals(mw2));
            Assert.IsTrue(mw2.Equals(mw1));
        }

        [TestMethod]
        public void EqualsTest()
        {
            var chars = new[] { MorseCharacter.A, MorseCharacter.M, MorseCharacter.Y, MorseCharacter.Number5 };
            var mw = new MorseWord(chars);
            Assert.IsFalse(mw.Equals(null));
        }

        #endregion
    }
}
