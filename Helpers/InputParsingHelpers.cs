using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helpers
{
    public static class InputParsingHelpers
    {
        public static IEnumerable<string> ParseFileAsStringArray(string path)
        {
            var reader = new StreamReader(path);
            return reader.ReadToEnd().Split('\n').Select(x => x.Replace("\r", ""));
        }

        public static IEnumerable<double> ParseFileAsDoubleArray(string path)
        {
            return ParseFileAsStringArray(path).Select(double.Parse);
        }
    }
}
