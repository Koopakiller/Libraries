using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Koopakiller.Linq;

namespace Koopakiller.Coding.Morse
{
    [DebuggerDisplay(@"{DebuggerDisplay}")]
    public class MorseCode : IFormattable, IEnumerable<MorseWord>, IEnumerable<string>
    {
        public IReadOnlyList<MorseWord> Words { get; }

        public MorseCode(params MorseWord[] words)
        {
            if (words == null)
            {
                words = new MorseWord[0];
            }
            this.Words = words;
        }

        public static MorseCode Parse(string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word), $"Parameter {nameof(word)} cannot be null.");
            }
            if (word.Length == 0)
            {
                return new MorseCode();
            }
            try
            {
                return new MorseCode(word.Split(' ').Select(MorseWord.Parse).ToArray());
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Parameter {nameof(word)} conatins at least one illegal character.", nameof(word), ex);
            }
        }


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
                case "m": // Morse   //? forwarded to MorseWord.ToString
                    return string.Join(new string(' ', (int)MorseSpace.Word), this.Words.Select(x => x.ToString(format, formatProvider)));
                case "c": // Chars   //? forwarded to MorseWord.ToString
                    return string.Join(" ", this.Words.Select(x => x.ToString(format, formatProvider)));
                case "d": // Debug
                    return string.Join(" ", this.Words.Select(x => x.ToString(format, formatProvider)));
                default:
                    throw new FormatException($"Invalid string-format '{nameof(format)}'.");
            }
        }

        public string ToString(string dit, string dah, string characterSeparator, string wordSeparator)
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
            if (wordSeparator == null)
            {
                throw new ArgumentNullException(nameof(wordSeparator), $"Parameter {nameof(wordSeparator)} cannot be null.");
            }
            return string.Join(wordSeparator, this.Words.Select(x => x.ToString(dit, dah, characterSeparator)));
        }

        #region IEnumerable

        public IEnumerator<MorseWord> GetEnumerator() => this.Words.GetEnumerator();

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => this.Words.Select(x => x.ToString("c")).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion

        public override int GetHashCode()
        {
            return this.Words.GetSequenceHashCode();
        }

        public override bool Equals(object obj)
        {
            var mc = obj as MorseCode;
            return mc != null && mc.Words.SequenceEqual(this.Words);
        }

        // ReSharper disable once UnusedMember.Local
        private string DebuggerDisplay => this.ToString("d");
    }
}