using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Koopakiller.Portable.Coding.Morse
{
    public class MorseCode : IFormattable, IEnumerable<MorseWord>, IEnumerable<string>
    {
        public IReadOnlyList<MorseWord> Words { get; }

        public MorseCode(params MorseWord[] words)
        {
            this.Words = words;
        }

        public static MorseCode Parse(string word)
        {
            return new MorseCode(word.Split(' ').Select(MorseWord.Parse).ToArray());
        }


        public override string ToString() => this.ToString("");

        public string ToString(string format) => this.ToString("", CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "":
                case "m": // Morse   //? forwarded to MorseWord.ToString
                case "c": // Chars   //? forwarded to MorseWord.ToString
                    return string.Join(new string(' ', (int)MorseSpace.Word), this.Words.Select(x => x.ToString(format, formatProvider)));
                case "d": // Debug
                    return $"{this.ToString("m")} ({this.ToString("c")})";
                default:
                    throw new FormatException($"Invalid string-format '{nameof(format)}'.");
            }
        }

        public string ToString(string dit, string dah, string signalSeperator, string characterSeparator, string wordSeparator)
        {
            return string.Join(wordSeparator, this.Words.Select(x => x.ToString(dit, dah, characterSeparator)));
        }

        #region IEnumerable

        public IEnumerator<MorseWord> GetEnumerator() => this.Words.GetEnumerator();

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => this.Words.Select(x => x.ToString()).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion
    }
}