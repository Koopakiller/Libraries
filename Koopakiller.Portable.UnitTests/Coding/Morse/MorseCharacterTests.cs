using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Koopakiller.Coding.Morse;
using Koopakiller.Linq;
using Koopakiller.Portable.UnitTests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ObjectCreationAsStatement
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'

namespace Koopakiller.Portable.UnitTests.Coding.Morse
{
    [TestClass]
    public class MorseCharacterTests
    {
        #region .ctor

        [TestMethod]
        public void Constructor()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            var mc = new MorseCharacter(signals);
            Assert.IsTrue(mc.Signals.SequenceEqual(signals));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullArray()
        {
            new MorseCharacter(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_EmptyArray()
        {
            var emtyArray = new MorseSignal[0];
            new MorseCharacter(emtyArray);
        }

        #endregion

        #region IEnumerable

        [TestMethod]
        public void IEnumerable_GetEnumerator()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            IEnumerable mc = new MorseCharacter(signals);
            Assert.IsTrue(mc.OfType<MorseSignal>().SequenceEqual(signals));
        }

        [TestMethod]
        public void IEnumerable_MorseSignal_GetEnumerator()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            IEnumerable<MorseSignal> mc = new MorseCharacter(signals);
            Assert.IsTrue(mc.SequenceEqual(signals));
        }

        #endregion

        #region ToString

        [TestMethod]
        public void ToString_NoParameter()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("−·−·", mc.ToString());
        }

        [TestMethod]
        public void ToString_EmptyStringFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("−·−·", mc.ToString(""));
        }

        [TestMethod]
        public void ToString_mFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("−·−·", mc.ToString("m"));
        }

        [TestMethod]
        public void ToString_dFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("−·−· (C)", mc.ToString("d"));
        }

        [TestMethod]
        public void ToString_cFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("C", mc.ToString("c"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_NullFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("C", mc.ToString(null));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToString_InvalidFormat()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual("C", mc.ToString("INVALID"));
        }

        #endregion

        #region ToChar

        [TestMethod]
        public void ToChar()
        {
            var mc = MorseCharacter.C;
            Assert.AreEqual('C', mc.ToChar());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ToChar_InvalidSequence()
        {
            var mc = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);
            Assert.AreEqual('C', mc.ToChar());
        }

        #endregion

        #region FromChar

        [TestMethod]
        public void FromChar_UppercaseCharacters()
        {
            Assert.AreEqual(MorseCharacter.A, MorseCharacter.FromChar('A'));
            Assert.AreEqual(MorseCharacter.B, MorseCharacter.FromChar('B'));
            Assert.AreEqual(MorseCharacter.C, MorseCharacter.FromChar('C'));
            Assert.AreEqual(MorseCharacter.D, MorseCharacter.FromChar('D'));
            Assert.AreEqual(MorseCharacter.E, MorseCharacter.FromChar('E'));
            Assert.AreEqual(MorseCharacter.F, MorseCharacter.FromChar('F'));
            Assert.AreEqual(MorseCharacter.G, MorseCharacter.FromChar('G'));
            Assert.AreEqual(MorseCharacter.H, MorseCharacter.FromChar('H'));
            Assert.AreEqual(MorseCharacter.I, MorseCharacter.FromChar('I'));
            Assert.AreEqual(MorseCharacter.J, MorseCharacter.FromChar('J'));
            Assert.AreEqual(MorseCharacter.K, MorseCharacter.FromChar('K'));
            Assert.AreEqual(MorseCharacter.L, MorseCharacter.FromChar('L'));
            Assert.AreEqual(MorseCharacter.M, MorseCharacter.FromChar('M'));
            Assert.AreEqual(MorseCharacter.N, MorseCharacter.FromChar('N'));
            Assert.AreEqual(MorseCharacter.O, MorseCharacter.FromChar('O'));
            Assert.AreEqual(MorseCharacter.P, MorseCharacter.FromChar('P'));
            Assert.AreEqual(MorseCharacter.Q, MorseCharacter.FromChar('Q'));
            Assert.AreEqual(MorseCharacter.R, MorseCharacter.FromChar('R'));
            Assert.AreEqual(MorseCharacter.S, MorseCharacter.FromChar('S'));
            Assert.AreEqual(MorseCharacter.T, MorseCharacter.FromChar('T'));
            Assert.AreEqual(MorseCharacter.U, MorseCharacter.FromChar('U'));
            Assert.AreEqual(MorseCharacter.V, MorseCharacter.FromChar('V'));
            Assert.AreEqual(MorseCharacter.W, MorseCharacter.FromChar('W'));
            Assert.AreEqual(MorseCharacter.X, MorseCharacter.FromChar('X'));
            Assert.AreEqual(MorseCharacter.Y, MorseCharacter.FromChar('Y'));
            Assert.AreEqual(MorseCharacter.Z, MorseCharacter.FromChar('Z'));
        }

        [TestMethod]
        public void FromChar_LowercaseCharacters()
        {
            Assert.AreEqual(MorseCharacter.A, MorseCharacter.FromChar('a'));
            Assert.AreEqual(MorseCharacter.B, MorseCharacter.FromChar('b'));
            Assert.AreEqual(MorseCharacter.C, MorseCharacter.FromChar('c'));
            Assert.AreEqual(MorseCharacter.D, MorseCharacter.FromChar('d'));
            Assert.AreEqual(MorseCharacter.E, MorseCharacter.FromChar('e'));
            Assert.AreEqual(MorseCharacter.F, MorseCharacter.FromChar('f'));
            Assert.AreEqual(MorseCharacter.G, MorseCharacter.FromChar('g'));
            Assert.AreEqual(MorseCharacter.H, MorseCharacter.FromChar('h'));
            Assert.AreEqual(MorseCharacter.I, MorseCharacter.FromChar('i'));
            Assert.AreEqual(MorseCharacter.J, MorseCharacter.FromChar('j'));
            Assert.AreEqual(MorseCharacter.K, MorseCharacter.FromChar('k'));
            Assert.AreEqual(MorseCharacter.L, MorseCharacter.FromChar('l'));
            Assert.AreEqual(MorseCharacter.M, MorseCharacter.FromChar('m'));
            Assert.AreEqual(MorseCharacter.N, MorseCharacter.FromChar('n'));
            Assert.AreEqual(MorseCharacter.O, MorseCharacter.FromChar('o'));
            Assert.AreEqual(MorseCharacter.P, MorseCharacter.FromChar('p'));
            Assert.AreEqual(MorseCharacter.Q, MorseCharacter.FromChar('q'));
            Assert.AreEqual(MorseCharacter.R, MorseCharacter.FromChar('r'));
            Assert.AreEqual(MorseCharacter.S, MorseCharacter.FromChar('s'));
            Assert.AreEqual(MorseCharacter.T, MorseCharacter.FromChar('t'));
            Assert.AreEqual(MorseCharacter.U, MorseCharacter.FromChar('u'));
            Assert.AreEqual(MorseCharacter.V, MorseCharacter.FromChar('v'));
            Assert.AreEqual(MorseCharacter.W, MorseCharacter.FromChar('w'));
            Assert.AreEqual(MorseCharacter.X, MorseCharacter.FromChar('x'));
            Assert.AreEqual(MorseCharacter.Y, MorseCharacter.FromChar('y'));
            Assert.AreEqual(MorseCharacter.Z, MorseCharacter.FromChar('z'));
        }

        [TestMethod]
        public void FromChar_Digits()
        {
            Assert.AreEqual(MorseCharacter.Number1, MorseCharacter.FromChar('1'));
            Assert.AreEqual(MorseCharacter.Number2, MorseCharacter.FromChar('2'));
            Assert.AreEqual(MorseCharacter.Number3, MorseCharacter.FromChar('3'));
            Assert.AreEqual(MorseCharacter.Number4, MorseCharacter.FromChar('4'));
            Assert.AreEqual(MorseCharacter.Number5, MorseCharacter.FromChar('5'));
            Assert.AreEqual(MorseCharacter.Number6, MorseCharacter.FromChar('6'));
            Assert.AreEqual(MorseCharacter.Number7, MorseCharacter.FromChar('7'));
            Assert.AreEqual(MorseCharacter.Number8, MorseCharacter.FromChar('8'));
            Assert.AreEqual(MorseCharacter.Number9, MorseCharacter.FromChar('9'));
            Assert.AreEqual(MorseCharacter.Number0, MorseCharacter.FromChar('0'));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FromChar_InvalidChar()
        {
            MorseCharacter.FromChar('~');
        }

        #endregion

        #region Static Character Properties

        [TestMethod]
        public void StaticCharacterProperties_Characters()
        {
            Assert.IsTrue(MorseCharacter.A.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.B.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.C.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.D.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.E.Signals.SequenceEqual(new[] { MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.F.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.G.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.H.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.I.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.J.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.K.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.L.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.M.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.N.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.O.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.P.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Q.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.R.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.S.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.T.Signals.SequenceEqual(new[] { MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.U.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.V.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.W.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.X.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Y.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Z.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, }));
        }

        [TestMethod]
        public void StaticCharacterProperties_Digits()
        {
            Assert.IsTrue(MorseCharacter.Number1.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Number2.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Number3.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Number4.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, }));
            Assert.IsTrue(MorseCharacter.Number5.Signals.SequenceEqual(new[] { MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Number6.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Number7.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Number8.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Number9.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, }));
            Assert.IsTrue(MorseCharacter.Number0.Signals.SequenceEqual(new[] { MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, }));
        }

        #endregion

        #region GetHashCode / Equals

        [TestMethod]
        public void GetHashCodeTest()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            var mc = new MorseCharacter(signals);
            Assert.AreEqual(signals.GetSequenceHashCode(), mc.GetHashCode());
        }

        [TestMethod]
        public void EqualsTestNullParameter()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            var mc1 = new MorseCharacter(signals);
            var mc2 = new MorseCharacter(signals);
            Assert.IsTrue(mc1.Equals(mc2));
            Assert.IsTrue(mc2.Equals(mc1));
        }

        [TestMethod]
        public void EqualsTest()
        {
            var signals = new[] { MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit };
            var mc = new MorseCharacter(signals);
            Assert.IsFalse(mc.Equals(null));
        }

        #endregion
    }
}
