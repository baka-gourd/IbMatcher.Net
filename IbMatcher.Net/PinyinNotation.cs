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
        /// 简拼 - Ascii notation but only using the first letter of each syllable (e.g., "p", "y")
        /// </summary>
        AsciiFirstLetter = 0x1,

        /// <summary>
        /// 全拼 - Full ASCII notation (e.g., "pin", "yin")
        /// </summary>
        Ascii = 0x2,

        /// <summary>
        /// 带声调全拼 - ASCII notation with numbers (e.g., "pin1", "yin1")
        /// </summary>
        AsciiTone = 0x4,

        /// <summary>
        /// Unicode with tone marks (e.g., "pīn", "yīn")
        /// </summary>
        Unicode = 0x8,

        /// <summary>
        /// 智能 ABC 双拼
        /// </summary>
        DiletterAbc = 0x10,

        /// <summary>
        /// 拼音加加双拼
        /// </summary>
        DiletterJiajia = 0x20,

        /// <summary>
        /// 微软双拼
        /// </summary>
        DiletterMicrosoft = 0x40,

        /// <summary>
        /// 华宇双拼（紫光双拼）
        /// </summary>
        DiletterThunisoft = 0x80,

        /// <summary>
        /// 小鹤双拼
        /// </summary>
        DiletterXiaohe = 0x100,

        /// <summary>
        /// 自然码双拼
        /// </summary>
        DiletterZrm = 0x200,

        /// <summary>
        /// Common set of notations: Ascii and AsciiFirstLetter
        /// </summary>
        Common = Ascii | AsciiFirstLetter
    }
}
