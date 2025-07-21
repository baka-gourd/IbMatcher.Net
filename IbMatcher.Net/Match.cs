using System;

namespace IbMatcher.Net
{
    /// <summary>
    /// Represents a match result from the IbMatcher.
    /// </summary>
    public readonly struct Match
    {
        /// <summary>
        /// The start index of the match in the haystack.
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// The end index of the match in the haystack.
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Indicates whether the match is a partial pattern match.
        /// </summary>
        /// <remarks>
        /// A partial match occurs when the pattern is matched with a partial syllable,
        /// for example pattern "pinyi" matching "拼音" (pinyin).
        /// </remarks>
        public bool IsPatternPartial { get; }

        /// <summary>
        /// Creates a new Match instance.
        /// </summary>
        /// <param name="start">The start index of the match</param>
        /// <param name="end">The end index of the match</param>
        /// <param name="isPatternPartial">Whether the match is a partial pattern match</param>
        internal Match(int start, int end, bool isPatternPartial)
        {
            Start = start;
            End = end;
            IsPatternPartial = isPatternPartial;
        }

        /// <summary>
        /// Gets the length of the match.
        /// </summary>
        public int Length => End - Start;

        /// <summary>
        /// Gets the matched substring from the original haystack.
        /// </summary>
        /// <param name="haystack">The original string that was searched</param>
        /// <returns>The matched substring</returns>
        public string GetMatchedText(string haystack)
        {
            if (haystack == null)
                throw new ArgumentNullException(nameof(haystack));

            // Adjust indices to ensure they are within valid range
            int safeStart = Math.Max(0, Math.Min(Start, haystack.Length));
            int safeEnd = Math.Max(safeStart, Math.Min(End, haystack.Length));

            return haystack.Substring(safeStart, safeEnd - safeStart);
        }

        /// <summary>
        /// Returns a string representation of the match.
        /// </summary>
        public override string ToString()
        {
            return $"Match(Start={Start}, End={End}, Length={Length}, IsPatternPartial={IsPatternPartial})";
        }
    }
}
