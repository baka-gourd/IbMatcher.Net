using System;

namespace IbMatcher.Net
{
    /// <summary>
    /// A multilingual and fast string matcher, supporting Chinese pinyin and Japanese romaji matching.
    /// </summary>
    public sealed class IbMatcher : IDisposable
    {
        private NativeMethods.IbMatcherHandle _handle;
        private bool _disposed;
        private readonly string _pattern;

        /// <summary>
        /// Creates a new instance of the IbMatcher class with the specified pattern and default configuration.
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        public IbMatcher(string pattern) : this(pattern, new IbMatcherConfig()) { }

        /// <summary>
        /// Creates a new instance of the IbMatcher class with the specified pattern and configuration.
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        /// <param name="config">The configuration to use</param>
        /// <exception cref="ArgumentNullException">Thrown if pattern or config is null</exception>
        /// <exception cref="InvalidOperationException">Thrown if the native matcher could not be created</exception>
        public IbMatcher(string pattern, IbMatcherConfig config)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _pattern = pattern;

            var nativeConfig = config.ToNative();

            _handle = NativeMethods.ib_matcher_new_utf16(
                pattern,
                (UIntPtr)pattern.Length,
                ref nativeConfig);

            if (_handle.IsInvalid)
                throw new InvalidOperationException("Failed to create IbMatcher instance");
        }

        /// <summary>
        /// Checks if the pattern matches anywhere in the haystack.
        /// </summary>
        /// <param name="haystack">The string to search in</param>
        /// <returns>True if the pattern matches anywhere in the haystack, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if haystack is null</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the matcher has been disposed</exception>
        public bool IsMatch(string haystack)
        {
            if (haystack == null)
                throw new ArgumentNullException(nameof(haystack));

            EnsureNotDisposed();

            return NativeMethods.ib_matcher_is_match_utf16(
                _handle,
                haystack,
                (UIntPtr)haystack.Length);
        }

        /// <summary>
        /// Finds the first match of the pattern in the haystack.
        /// </summary>
        /// <param name="haystack">The string to search in</param>
        /// <returns>A Match object if a match is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if haystack is null</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the matcher has been disposed</exception>
        public Match? Find(string haystack)
        {
            if (haystack == null)
                throw new ArgumentNullException(nameof(haystack));

            EnsureNotDisposed();

            var result = NativeMethods.ib_matcher_find_utf16(
                _handle,
                haystack,
                (UIntPtr)haystack.Length);

            if (!result.Found)
                return null;

            // Convert UIntPtr to int safely
            int start = IntPtr.Size == 4 ? (int)result.Start.ToUInt32() : (int)result.Start.ToUInt64();
            int end = IntPtr.Size == 4 ? (int)result.End.ToUInt32() : (int)result.End.ToUInt64();

            return new Match(
                start,
                end,
                result.IsPatternPartial);
        }

        /// <summary>
        /// Tests if the pattern matches at the start of the haystack.
        /// </summary>
        /// <param name="haystack">The string to test</param>
        /// <returns>A Match object if a match is found, null otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if haystack is null</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the matcher has been disposed</exception>
        public Match? Test(string haystack)
        {
            if (haystack == null)
                throw new ArgumentNullException(nameof(haystack));

            EnsureNotDisposed();

            var result = NativeMethods.ib_matcher_test_utf16(
                _handle,
                haystack,
                (UIntPtr)haystack.Length);

            if (!result.Found)
                return null;

            // Convert UIntPtr to int safely
            int start = IntPtr.Size == 4 ? (int)result.Start.ToUInt32() : (int)result.Start.ToUInt64();
            int end = IntPtr.Size == 4 ? (int)result.End.ToUInt32() : (int)result.End.ToUInt64();

            return new Match(
                start,
                end,
                result.IsPatternPartial);
        }

        /// <summary>
        /// Creates an IbMatcher with pinyin matching enabled.
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        /// <param name="notations">The pinyin notations to use</param>
        /// <returns>A new IbMatcher instance</returns>
        public static IbMatcher CreatePinyinMatcher(string pattern, PinyinNotation notations = PinyinNotation.Common)
        {
            return new IbMatcher(pattern, IbMatcherConfig.WithPinyin(notations));
        }

        /// <summary>
        /// Creates an IbMatcher with romaji matching enabled.
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        /// <returns>A new IbMatcher instance</returns>
        public static IbMatcher CreateRomajiMatcher(string pattern)
        {
            return new IbMatcher(pattern, IbMatcherConfig.WithRomaji());
        }

        /// <summary>
        /// Creates an IbMatcher with both pinyin and romaji matching enabled.
        /// </summary>
        /// <param name="pattern">The pattern to search for</param>
        /// <param name="notations">The pinyin notations to use</param>
        /// <returns>A new IbMatcher instance</returns>
        public static IbMatcher CreateMultilingualMatcher(string pattern, PinyinNotation notations = PinyinNotation.Common)
        {
            return new IbMatcher(pattern, IbMatcherConfig.WithPinyinAndRomaji(notations));
        }

        /// <summary>
        /// Disposes the native resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                if (!_handle.IsInvalid)
                {
                    NativeMethods.ib_matcher_free(_handle);
                    _handle = new NativeMethods.IbMatcherHandle { Handle = IntPtr.Zero };
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizer to ensure resources are cleaned up if Dispose is not called.
        /// </summary>
        ~IbMatcher()
        {
            Dispose();
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(IbMatcher), "This IbMatcher instance has been disposed");

            if (_handle.IsInvalid)
                throw new InvalidOperationException("IbMatcher handle is invalid");
        }

        /// <summary>
        /// Returns a string representation of the matcher.
        /// </summary>
        public override string ToString()
        {
            return $"IbMatcher(Pattern=\"{_pattern}\")";
        }
    }
}
