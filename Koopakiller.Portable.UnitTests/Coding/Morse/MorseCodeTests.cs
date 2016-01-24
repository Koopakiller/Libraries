using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Koopakiller.Coding.Morse;
using Koopakiller.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Koopakiller.Portable.UnitTests.Coding.Morse
{
    [TestClass]
    public class MorseCodeTests
    {
        #region Helper

        private MorseWord[] GetSampleSentence()
        {
            return new[]
            {
                new MorseWord(MorseCharacter.A, MorseCharacter.B, MorseCharacter.C, MorseCharacter.D),
                new MorseWord(MorseCharacter.W, MorseCharacter.X, MorseCharacter.Y, MorseCharacter.Z),
                new MorseWord(MorseCharacter.Number2, MorseCharacter.Number5, MorseCharacter.Number8)
            };
        }

        #endregion

        #region .ctor

        [TestMethod]
        public void Constructor()
        {
            var words = this.GetSampleSentence();
            var mc = new MorseCode(words);

            Assert.IsTrue(mc.Words.SequenceEqual(words));
        }

        [TestMethod]
        public void Constructor_NullArray()
        {
            var mw = new MorseCode(null);
            Assert.AreEqual(0, mw.Words.Count);
        }

        #endregion

        #region IEnumerable

        [TestMethod]
        public void IEnumerable_GetEnumerator()
        {
            var words = this.GetSampleSentence();
            var mw = new MorseCode(words)as IEnumerable;
            Assert.IsTrue(mw.OfType<MorseWord>().SequenceEqual(words));
        }

        [TestMethod]
        public void IEnumerable_MorseWord_GetEnumerator()
        {
            var words = this.GetSampleSentence();
            var mw = new MorseCode(words)as IEnumerable<MorseWord>;
            Assert.IsTrue(mw.SequenceEqual(words));
        }

        [TestMethod]
        public void IEnumerable_String_GetEnumerator()
        {
            var words = this.GetSampleSentence();
            var mw = new MorseCode(words)as IEnumerable<string>;
            Assert.IsTrue(mw.SequenceEqual(new[] { "ABCD", "WXYZ", "258" }));
        }

        #endregion

        #region ToString

        [TestMethod]
        public void ToString_NoParameter()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            Assert.AreEqual("·− −··· −·−· −··       ·−− −··− −·−− −−··       ··−−− ····· −−−··", mc.ToString());
        }

        [TestMethod]
        public void ToString_EmptyStringFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            Assert.AreEqual("·− −··· −·−· −··       ·−− −··− −·−− −−··       ··−−− ····· −−−··", mc.ToString(""));
        }

        [TestMethod]
        public void ToString_mFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            Assert.AreEqual("·− −··· −·−· −··       ·−− −··− −·−− −−··       ··−−− ····· −−−··", mc.ToString("m"));
        }

        [TestMethod]
        public void ToString_dFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            Assert.AreEqual("·− −··· −·−· −·· (ABCD) ·−− −··− −·−− −−·· (WXYZ) ··−−− ····· −−−·· (258)", mc.ToString("d"));
        }

        [TestMethod]
        public void ToString_cFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            Assert.AreEqual("ABCD WXYZ 258", mc.ToString("c"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_NullFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            mc.ToString(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToString_InvalidFormat()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            mc.ToString("INVALID");
        }

        #region CustomDitDah

        [TestMethod]
        public void ToString_CustomDitDah()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var r = mc.ToString("A", "B", "C", "D");
            Assert.AreEqual("ABCBAAACBABACBAADABBCBAABCBABBCBBAADAABBBCAAAAACBBBAA", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullDit()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var r = mc.ToString(null, "B", "C", "D");
            Assert.AreEqual("", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullDah()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var r = mc.ToString("A", null, "C", "D");
            Assert.AreEqual("", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullCharacterSeparator()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var r = mc.ToString("A", "B", null, "D");
            Assert.AreEqual("", r);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_CustomDitDah_NullWordSeparator()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var r = mc.ToString("A", "B", "C", null);
            Assert.AreEqual("", r);
        }

        #endregion

        #endregion

        #region Parse

        [TestMethod]
        public void Parse()
        {
            var mcShould = new MorseCode(this.GetSampleSentence());
            var mc = MorseCode.Parse("ABCD WXYZ 258");
            Assert.AreEqual(mcShould, mc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_IllegalCharacter()
        {
            MorseCode.Parse("~ ~");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_Null()
        {
            MorseCode.Parse(null);
        }

        [TestMethod]
        public void Parse_Empty()
        {
            var mw = MorseCode.Parse("");
            Assert.AreEqual(new MorseCode(), mw);
        }

        #endregion

        #region GetHashCode / Equals

        [TestMethod]
        public void GetHashCodeTest()
        {
            var words = this.GetSampleSentence();
            var mc = new MorseCode(words);
            Assert.AreEqual(words.GetSequenceHashCode(), mc.GetHashCode());
        }

        [TestMethod]
        public void EqualsTestNullParameter()
        {
            var words = this.GetSampleSentence();
            var mc1 = new MorseCode(words);
            var mc2 = new MorseCode(words);
            Assert.IsTrue(mc1.Equals(mc2));
            Assert.IsTrue(mc2.Equals(mc1));
        }

        [TestMethod]
        public void EqualsTest()
        {
            var words = this.GetSampleSentence();
            var mc = new MorseCode(words);
            Assert.IsFalse(mc.Equals(null));
        }

        #endregion

        #region DebuggerDisplay

        [TestMethod]
        public void DebuggerDisplay()
        {
            var mc = new MorseCode(this.GetSampleSentence());
            var po = new PrivateObject(mc);
            Assert.AreEqual(mc.ToString("d"), po.GetProperty("DebuggerDisplay"));
        }

        #endregion
    }
}
