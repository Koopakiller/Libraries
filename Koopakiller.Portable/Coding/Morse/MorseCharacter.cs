using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Koopakiller.Linq;

namespace Koopakiller.Coding.Morse
{
    public class MorseCharacter : IFormattable, IEnumerable<MorseSignal>
    {
        public IReadOnlyList<MorseSignal> Signals { get; }

        public MorseCharacter(params MorseSignal[] signals)
        {
            if (signals == null)
            {
                throw new ArgumentNullException(nameof(signals), $"Parameter {nameof(signals)} cannot be null.");
            }
            if (signals.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(signals), $"Parameter {nameof(signals)} cannot be empty.");
            }
            this.Signals = signals;
        }

        public static MorseCharacter FromChar(char chr)
        {
            chr = char.ToUpper(chr);
            var mc = MorseCharacter.KnownCharacters.FirstOrDefault(x => x.Key == chr);
            if (mc.Value == null)
            {
                throw new ArgumentOutOfRangeException(nameof(chr), chr, "Unsupported character.");
            }
            return mc.Value;
        }

        #region IEnumerable

        public IEnumerator<MorseSignal> GetEnumerator() => this.Signals.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion

        public override string ToString() => this.ToString("");

        public string ToString(string format) => this.ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format), $"Parameter {nameof(format)} cannot be null.");
            }
            switch (format)
            {
                case "":
                case "m": // Morse
                    return this.ToString("·", "−");
                case "d": // Debug
                    return $"{this.ToString("m")} ({this.ToString("c")})";
                case "c": // Character
                    return this.ToChar().ToString();
                default:
                    throw new FormatException($"Invalid string-format '{nameof(format)}'.");
            }
        }

        public string ToString(string dit, string dah)
        {
            return string.Concat(this.Signals.Select(x => x == MorseSignal.Dit ? dit : dah));
        }

        public char ToChar()
        {
            var chr = MorseCharacter.KnownCharacters.FirstOrDefault(x => x.Value.Signals.SequenceEqual(this.Signals));
            if (chr.Value == null)
            {
                throw new NotSupportedException("Unknown signal sequence");
            }
            return chr.Key;
        }

        #region Static Properties

        public static MorseCharacter A { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter B { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter C { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter D { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter E { get; } = new MorseCharacter(MorseSignal.Dit);
        public static MorseCharacter F { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter G { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter H { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter I { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter J { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter K { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter L { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter M { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter N { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter O { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter P { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter Q { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter R { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter S { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter T { get; } = new MorseCharacter(MorseSignal.Dah);
        public static MorseCharacter U { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter V { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter W { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter X { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter Y { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter Z { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit);


        public static MorseCharacter Number1 { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter Number2 { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter Number3 { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah, MorseSignal.Dah);
        public static MorseCharacter Number4 { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dah);
        public static MorseCharacter Number5 { get; } = new MorseCharacter(MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter Number6 { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter Number7 { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter Number8 { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit, MorseSignal.Dit);
        public static MorseCharacter Number9 { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dit);
        public static MorseCharacter Number0 { get; } = new MorseCharacter(MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah, MorseSignal.Dah);

        private static readonly Dictionary<char, MorseCharacter> KnownCharacters = new Dictionary<char, MorseCharacter>
        {
            ['A'] = MorseCharacter.A,
            ['B'] = MorseCharacter.B,
            ['C'] = MorseCharacter.C,
            ['D'] = MorseCharacter.D,
            ['E'] = MorseCharacter.E,
            ['F'] = MorseCharacter.F,
            ['G'] = MorseCharacter.G,
            ['H'] = MorseCharacter.H,
            ['I'] = MorseCharacter.I,
            ['J'] = MorseCharacter.J,
            ['K'] = MorseCharacter.K,
            ['L'] = MorseCharacter.L,
            ['M'] = MorseCharacter.M,
            ['N'] = MorseCharacter.N,
            ['O'] = MorseCharacter.O,
            ['P'] = MorseCharacter.P,
            ['Q'] = MorseCharacter.Q,
            ['R'] = MorseCharacter.R,
            ['S'] = MorseCharacter.S,
            ['T'] = MorseCharacter.T,
            ['U'] = MorseCharacter.U,
            ['V'] = MorseCharacter.V,
            ['W'] = MorseCharacter.W,
            ['X'] = MorseCharacter.X,
            ['Y'] = MorseCharacter.Y,
            ['Z'] = MorseCharacter.Z,
            ['1'] = MorseCharacter.Number1,
            ['2'] = MorseCharacter.Number2,
            ['3'] = MorseCharacter.Number3,
            ['4'] = MorseCharacter.Number4,
            ['5'] = MorseCharacter.Number5,
            ['6'] = MorseCharacter.Number6,
            ['7'] = MorseCharacter.Number7,
            ['8'] = MorseCharacter.Number8,
            ['9'] = MorseCharacter.Number9,
            ['0'] = MorseCharacter.Number0,
        };

        #endregion

        public override int GetHashCode()
        {
            return this.Signals.GetSequenceHashCode();
        }

        public override bool Equals(object obj)
        {
            var mc = obj as MorseCharacter;
            return mc != null && mc.Signals.SequenceEqual(this.Signals);
        }
    }
}
