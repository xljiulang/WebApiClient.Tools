using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Class = {Class}")]
    public class Code
    {
        private readonly string code;

        public string Class { get; private set; }

        public Code(string code)
        {
            this.code = AutoIndent(code);
            this.Class = Regex.Match(code, @"(?<=class |enum |interface )\w+").Value;
        }

        public IEnumerable<string> Lines
        {
            get => GetLines(this.code);
        }

        public override string ToString()
        {
            return this.code;
        }

        private static string AutoIndent(string source)
        {
            if (source == null)
            {
                return null;
            }

            var tab = 0;
            var builder = new StringBuilder();
            var compactCode = Compact(source);
            foreach (var line in GetLines(compactCode))
            {
                var cTab = tab;
                if (line == "{")
                {
                    tab = tab + 1;
                }
                else if (line == "}")
                {
                    cTab = tab - 1;
                    tab = tab - 1;
                }

                var prefix = string.Empty.PadRight(cTab * 4, ' ');
                var suffix = line.EndsWith(");") ? Environment.NewLine : null;
                builder.AppendLine($"{prefix}{line}{suffix}");
            }
            return builder.ToString();
        }

        private static string Compact(string source)
        {
            if (source == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            foreach (var line in GetLines(source))
            {
                builder.AppendLine(line.Trim());
            }
            var code = builder.ToString();
            return Regex.Replace(code, $@"{Environment.NewLine}\s*{Environment.NewLine}", Environment.NewLine);
        }


        public static IEnumerable<string> GetLines(string source)
        {
            if (source == null)
            {
                yield break;
            }

            using (var reader = new StringReader(source))
            {
                while (reader.Peek() >= 0)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}
