using System.IO;

namespace IbMatcher.Net
{
    public class IbDllConfig
    {
        private static string? _dllPath;

        public static string? DllPath
        {
            get => _dllPath;
            set
            {
                if (value != null && !File.Exists(value))
                {
                    throw new FileNotFoundException($"The specified DLL file does not exist: {value}");
                }
                _dllPath = value;
            }
        }
    }
}