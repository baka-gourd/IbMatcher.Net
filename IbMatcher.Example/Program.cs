using IbMatcher.Net;

namespace IbMatcher.Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IbBridge Example - C# bindings for ib-matcher");
            Console.WriteLine("===========================================");

            // Example 1: Basic pinyin matching
            Console.WriteLine("\nExample 1: Basic Pinyin Matching");
            Console.WriteLine("-------------------------------");
            using (var matcher = Net.IbMatcher.CreatePinyinMatcher("pysousuoeve"))
            {
                string haystack = "拼音搜索Everything";
                bool isMatch = matcher.IsMatch(haystack);
                Console.WriteLine($"Pattern: \"pysousuoeve\"");
                Console.WriteLine($"Haystack: \"{haystack}\"");
                Console.WriteLine($"Match result: {isMatch}");

                var match = matcher.Find(haystack);
                if (match.HasValue)
                {
                    Console.WriteLine($"Match position: [{match.Value.Start}..{match.Value.End}]");
                    Console.WriteLine($"Haystack length: {haystack.Length}");

                    Console.WriteLine($"Matched text: \"{match.Value.GetMatchedText(haystack)}\"");
                }
            }

            // Example 2: Romaji matching with partial pattern
            Console.WriteLine("\nExample 2: Romaji Matching");
            Console.WriteLine("-------------------------");
            var config = IbMatcherConfig.WithRomaji();
            config.IsPatternPartial = true;

            using (var matcher = new Net.IbMatcher("konosuba", config))
            {
                string haystack = "この素晴らしい世界に祝福を";
                bool isMatch = matcher.IsMatch(haystack);
                Console.WriteLine($"Pattern: \"konosuba\"");
                Console.WriteLine($"Haystack: \"{haystack}\"");
                Console.WriteLine($"Match result: {isMatch}");

                var match = matcher.Find(haystack);
                if (match.HasValue)
                {
                    Console.WriteLine($"Match position: [{match.Value.Start}..{match.Value.End}]");
                    Console.WriteLine($"Haystack length: {haystack.Length}");

                    Console.WriteLine($"Matched text: \"{match.Value.GetMatchedText(haystack)}\"");
                    Console.WriteLine($"Is pattern partial: {match.Value.IsPatternPartial}");
                }
            }

            // Example 3: Testing matches at the start of a string
            Console.WriteLine("\nExample 3: Testing Match at Start");
            Console.WriteLine("--------------------------------");
            using (var matcher = Net.IbMatcher.CreatePinyinMatcher("pin"))
            {
                string haystack = "拼音输入法";
                var match = matcher.Test(haystack);
                Console.WriteLine($"Pattern: \"pin\"");
                Console.WriteLine($"Haystack: \"{haystack}\"");

                if (match.HasValue)
                {
                    Console.WriteLine($"Match found at the start: [{match.Value.Start}..{match.Value.End}]");
                    Console.WriteLine($"Haystack length: {haystack.Length}");

                    Console.WriteLine($"Matched text: \"{match.Value.GetMatchedText(haystack)}\"");
                }
                else
                {
                    Console.WriteLine("No match at the start of the string");
                }
            }

            // Example 4: Using starts_with and ends_with constraints
            Console.WriteLine("\nExample 4: Using Constraints");
            Console.WriteLine("---------------------------");
            var constraintConfig = new IbMatcherConfig
            {
                EnablePinyin = true,
                PinyinNotations = PinyinNotation.Common,
                StartsWith = true  // Only match if the haystack starts with the pattern
            };

            using (var matcher = new Net.IbMatcher("pin", constraintConfig))
            {
                string haystack1 = "拼音输入法";
                string haystack2 = "输入拼音";

                Console.WriteLine($"Pattern: \"pin\" with StartsWith=true");
                Console.WriteLine($"Haystack 1: \"{haystack1}\"");
                Console.WriteLine($"Haystack 2: \"{haystack2}\"");

                bool isMatch1 = matcher.IsMatch(haystack1);
                bool isMatch2 = matcher.IsMatch(haystack2);

                Console.WriteLine($"Match result 1: {isMatch1}");
                Console.WriteLine($"Match result 2: {isMatch2}");
            }

            // Example 5: Mixed language matching
            Console.WriteLine("\nExample 5: Mixed Language Matching");
            Console.WriteLine("--------------------------------");
            using (var matcher = Net.IbMatcher.CreateMultilingualMatcher("hatsuneodxyy"))
            {
                string haystack = "初音殴打喜羊羊";
                var match = matcher.Find(haystack);
                Console.WriteLine($"Pattern: \"hatsuneodxyy\"");
                Console.WriteLine($"Haystack: \"{haystack}\"");

                if (match.HasValue)
                {
                    Console.WriteLine($"Match position: [{match.Value.Start}..{match.Value.End}]");
                    Console.WriteLine($"Haystack length: {haystack.Length}");

                    Console.WriteLine($"Matched text: \"{match.Value.GetMatchedText(haystack)}\"");
                    Console.WriteLine($"Is pattern partial: {match.Value.IsPatternPartial}");
                }
                else
                {
                    Console.WriteLine("No match found");
                }
            }
        }
    }
}
