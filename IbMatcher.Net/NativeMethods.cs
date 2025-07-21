using System;
using System.Runtime.InteropServices;

namespace IbMatcher.Net
{
    /// <summary>
    /// Native methods for interacting with the ib-bridge native library.
    /// </summary>
    internal static class NativeMethods
    {
        private const string DllName = "ib_matcher";

        /// <summary>
        /// Opaque handle type for IbMatcher
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct IbMatcherHandle
        {
            public IntPtr Handle;

            public static implicit operator IntPtr(IbMatcherHandle handle) => handle.Handle;
            public static implicit operator IbMatcherHandle(IntPtr ptr) => new IbMatcherHandle { Handle = ptr };

            public bool IsInvalid => Handle == IntPtr.Zero;
        }

        /// <summary>
        /// Match result structure returned from native methods
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MatchResult
        {
            public UIntPtr Start;
            public UIntPtr End;
            [MarshalAs(UnmanagedType.I1)]
            public bool IsPatternPartial;
            [MarshalAs(UnmanagedType.I1)]
            public bool Found;
        }

        /// <summary>
        /// Configuration struct for IbMatcher
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MatcherConfig
        {
            [MarshalAs(UnmanagedType.I1)]
            public bool Analyze;
            [MarshalAs(UnmanagedType.I1)]
            public bool IsPatternPartial;
            [MarshalAs(UnmanagedType.I1)]
            public bool StartsWith;
            [MarshalAs(UnmanagedType.I1)]
            public bool EndsWith;
            [MarshalAs(UnmanagedType.I1)]
            public bool CaseInsensitive;
            [MarshalAs(UnmanagedType.I1)]
            public bool MixLang;
            [MarshalAs(UnmanagedType.I1)]
            public bool EnablePinyin;
            public uint PinyinNotations;
            [MarshalAs(UnmanagedType.I1)]
            public bool PinyinCaseInsensitive;
            [MarshalAs(UnmanagedType.I1)]
            public bool EnableRomaji;
            [MarshalAs(UnmanagedType.I1)]
            public bool RomajiCaseInsensitive;
        }

        #region UTF-8 API

        /// <summary>
        /// Create a new IbMatcher with a UTF-8 pattern string
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern IbMatcherHandle ib_matcher_new(string pattern, ref MatcherConfig config);

        /// <summary>
        /// Free the IbMatcher instance
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ib_matcher_free(IbMatcherHandle handle);

        /// <summary>
        /// Check if the pattern matches anywhere in the haystack
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool ib_matcher_is_match(IbMatcherHandle handle, string haystack);

        /// <summary>
        /// Find the first match in the haystack
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern MatchResult ib_matcher_find(IbMatcherHandle handle, string haystack);

        /// <summary>
        /// Test if the pattern matches at the start of the haystack
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        internal static extern MatchResult ib_matcher_test(IbMatcherHandle handle, string haystack);

        #endregion

        #region UTF-16 API

        /// <summary>
        /// Create a new IbMatcher with a UTF-16 pattern string
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        internal static extern IbMatcherHandle ib_matcher_new_utf16(string pattern, UIntPtr patternLen, ref MatcherConfig config);

        /// <summary>
        /// Check if the pattern matches anywhere in the haystack (UTF-16)
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool ib_matcher_is_match_utf16(IbMatcherHandle handle, string haystack, UIntPtr haystackLen);

        /// <summary>
        /// Find the first match in the haystack (UTF-16)
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        internal static extern MatchResult ib_matcher_find_utf16(IbMatcherHandle handle, string haystack, UIntPtr haystackLen);

        /// <summary>
        /// Test if the pattern matches at the start of the haystack (UTF-16)
        /// </summary>
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        internal static extern MatchResult ib_matcher_test_utf16(IbMatcherHandle handle, string haystack, UIntPtr haystackLen);

        #endregion
    }
}
