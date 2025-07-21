namespace IbMatcher.Net
{
    /// <summary>
    /// Configuration options for creating an IbMatcher instance.
    /// </summary>
    public class IbMatcherConfig
    {
        /// <summary>
        /// Gets or sets whether to analyze the pattern for optimizations.
        /// </summary>
        /// <remarks>
        /// For matching more than 1000 strings, enabling analysis can optimize the pattern further.
        /// The analysis costs ~65us, equivalent to about 220~1100 matches.
        /// </remarks>
        public bool Analyze { get; set; } = false;

        /// <summary>
        /// Gets or sets whether the pattern can match pinyins/romajis starting with the ending of the pattern.
        /// </summary>
        /// <remarks>
        /// For example, pattern "pinyi" can match "拼音" (whose pinyin is "pinyin") if IsPatternPartial is true.
        /// </remarks>
        public bool IsPatternPartial { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to only match if the haystack starts with the pattern.
        /// </summary>
        public bool StartsWith { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to only match if the haystack ends with the pattern.
        /// </summary>
        public bool EndsWith { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to ignore case when matching plain text.
        /// </summary>
        /// <remarks>
        /// This applies to plain character matching, not pinyin or romaji matching.
        /// </remarks>
        public bool CaseInsensitive { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to allow matching a haystack with mixed languages (pinyin and romaji) at the same time.
        /// </summary>
        /// <remarks>
        /// Setting this to true may lead to unexpected matches, especially if AsciiFirstLetter is enabled,
        /// and may also result in lower performance.
        /// </remarks>
        public bool MixLang { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to enable pinyin matching.
        /// </summary>
        public bool EnablePinyin { get; set; } = false;

        /// <summary>
        /// Gets or sets the pinyin notations to use.
        /// </summary>
        /// <remarks>
        /// This is only used if EnablePinyin is true.
        /// </remarks>
        public PinyinNotation PinyinNotations { get; set; } = PinyinNotation.Common;

        /// <summary>
        /// Gets or sets whether to ignore case when matching pinyin text.
        /// </summary>
        /// <remarks>
        /// This is only used if EnablePinyin is true.
        /// </remarks>
        public bool PinyinCaseInsensitive { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to enable romaji matching.
        /// </summary>
        public bool EnableRomaji { get; set; } = false;

        /// <summary>
        /// Gets or sets whether to ignore case when matching romaji text.
        /// </summary>
        /// <remarks>
        /// This is only used if EnableRomaji is true.
        /// </remarks>
        public bool RomajiCaseInsensitive { get; set; } = true;

        /// <summary>
        /// Creates a new instance of the IbMatcherConfig class with default settings.
        /// </summary>
        public IbMatcherConfig() { }

        /// <summary>
        /// Creates a new configuration with pinyin matching enabled.
        /// </summary>
        /// <param name="notations">The pinyin notations to use</param>
        /// <returns>A new configuration instance</returns>
        public static IbMatcherConfig WithPinyin(PinyinNotation notations = PinyinNotation.Common)
        {
            return new IbMatcherConfig
            {
                EnablePinyin = true,
                PinyinNotations = notations
            };
        }

        /// <summary>
        /// Creates a new configuration with romaji matching enabled.
        /// </summary>
        /// <returns>A new configuration instance</returns>
        public static IbMatcherConfig WithRomaji()
        {
            return new IbMatcherConfig
            {
                EnableRomaji = true
            };
        }

        /// <summary>
        /// Creates a new configuration with both pinyin and romaji matching enabled.
        /// </summary>
        /// <param name="notations">The pinyin notations to use</param>
        /// <returns>A new configuration instance</returns>
        public static IbMatcherConfig WithPinyinAndRomaji(PinyinNotation notations = PinyinNotation.Common)
        {
            return new IbMatcherConfig
            {
                EnablePinyin = true,
                PinyinNotations = notations,
                EnableRomaji = true,
                MixLang = true
            };
        }

        /// <summary>
        /// Converts the configuration to the native format used by P/Invoke calls.
        /// </summary>
        internal NativeMethods.MatcherConfig ToNative()
        {
            return new NativeMethods.MatcherConfig
            {
                Analyze = Analyze,
                IsPatternPartial = IsPatternPartial,
                StartsWith = StartsWith,
                EndsWith = EndsWith,
                CaseInsensitive = CaseInsensitive,
                MixLang = MixLang,
                EnablePinyin = EnablePinyin,
                PinyinNotations = (uint)PinyinNotations,
                PinyinCaseInsensitive = PinyinCaseInsensitive,
                EnableRomaji = EnableRomaji,
                RomajiCaseInsensitive = RomajiCaseInsensitive
            };
        }
    }
}
