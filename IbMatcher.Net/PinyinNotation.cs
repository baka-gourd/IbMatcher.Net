using System;

namespace IbMatcher.Net
{
    /// <summary>
    /// Specifies the pinyin notation methods to use for matching.
    /// </summary>
    [Flags]
    public enum PinyinNotation : uint
    {
        /// <summary>
        /// No notation
        /// </summary>
        None = 0,

        /// <summary>
        /// Full ASCII notation (e.g., "pin yin")
        /// </summary>
        Ascii = 1 << 0,

        /// <summary>
        /// Ascii notation but only using the first letter of each syllable (e.g., "py")
        /// </summary>
        AsciiFirstLetter = 1 << 1,

        /// <summary>
        /// ASCII notation with numbers (e.g., "pin1 yin1")
        /// </summary>
        AsciiWithTone = 1 << 2,

        /// <summary>
        /// ASCII with first letter and tone number (e.g., "p1y1")
        /// </summary>
        AsciiFirstLetterWithTone = 1 << 3,

        /// <summary>
        /// Unicode with tone marks (e.g., "pīn yīn")
        /// </summary>
        Unicode = 1 << 4,

        /// <summary>
        /// Unicode with tone marks but only using the first letter of each syllable (e.g., "pī")
        /// </summary>
        UnicodeFirstLetter = 1 << 5,

        /// <summary>
        /// Common set of notations: Ascii and AsciiFirstLetter
        /// </summary>
        Common = Ascii | AsciiFirstLetter
    }
}
