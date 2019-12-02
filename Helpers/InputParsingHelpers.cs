using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helpers
{
    public static class InputParsingHelpers
    {
        public static IEnumerable<string> ParseFileAsStringArray(string path, char separator)
        {
            var reader = new StreamReader(path);
            return reader.ReadToEnd().Split(separator).Select(x => x.Replace("\r", ""));
        }

        public static IEnumerable<double> ParseFileAsDoubleArray(string path, char separator)
        {
            return ParseFileAsStringArray(path, separator).Select(double.Parse);
        }
        public static IEnumerable<int> ParseFileAsIntArray(string path, char separator)
        {
            return ParseFileAsStringArray(path, separator).Select(int.Parse);
        }
    }
}
