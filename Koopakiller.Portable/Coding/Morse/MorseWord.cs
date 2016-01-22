using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Koopakiller.Linq;

namespace Koopakiller.Coding.Morse
{
    public class MorseWord : IFormattable, IEnumerable<MorseCharacter>, IEnumerable<char>
    {
        public IReadOnlyList<MorseCharacter> Characters { get; }

        public MorseWord(params MorseCharacter[] chars)
        {
            if (chars == null)
            {
                chars = new MorseCharacter[0];
            }

            this.Characters = chars;
        }

        public static MorseWord Parse(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word), $"Parameter {nameof(word)} cannot be null.");
            }
            try
            {
                return new MorseWord(word.Select(MorseCharacter.FromChar).ToArray());
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException($"Parameter {nameof(word)} conatins at least one illegal character.", nameof(word), ex);
            }
        }

        #region IEnumerable

        public IEnumerator<MorseCharacter> GetEnumerator() => this.Characters.GetEnumerator();

        IEnumerator<char> IEnumerable<char>.GetEnumerator() => this.Characters.Select(x => x.ToChar()).GetEnumerator();

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
                case "m": // Morse   //? forwarded to MorseCharacter.ToString
                    return string.Join(new string(' ', (int)MorseSpace.Character), this.Characters.Select(x => x.ToString(format, formatProvider)));
                case "c": // Chars   //? forwarded to MorseCharacter.ToString
                    return string.Concat(this.Characters.Select(x => x.ToString(format, formatProvider)));
                case "d": // Debug
                    return $"{this.ToString("m")} ({this.ToString("c")})";
                default:
                    throw new FormatException($"Invalid string-format '{nameof(format)}'.");
            }
        }

        public string ToString(string dit, string dah, string characterSeparator)
        {
            if (dit == null)
            {
                throw new ArgumentNullException(nameof(dit), $"Parameter {nameof(dit)} cannot be null.");
            }
            if (dah == null)
            {
                throw new ArgumentNullException(nameof(dah), $"Parameter {nameof(dah)} cannot be null.");
            }
            if (characterSeparator == null)
            {
                throw new ArgumentNullException(nameof(characterSeparator), $"Parameter {nameof(characterSeparator)} cannot be null.");
            }
            return string.Join(characterSeparator, this.Characters.Select(x => x.ToString(dit, dah)));
        }

        public override int GetHashCode()
        {
            return this.Characters.GetSequenceHashCode();
        }

        public override bool Equals(object obj)
        {
            var mc = obj as MorseWord;
            return mc != null && mc.Characters.SequenceEqual(this.Characters);
        }
    }
}