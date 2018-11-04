using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示c#代码
    /// 自动代码美化
    /// </summary>
    [DebuggerDisplay("Class = {Class}")]
    public class CSharpCode
    {
        /// <summary>
        /// 源代码
        /// </summary>
        private readonly string code;

        /// <summary>
        /// 代码声明的类型名称
        /// </summary>
        public string Class { get; private set; }

        /// <summary>
        /// c#代码
        /// </summary>
        /// <param name="source">源代码</param>
        public CSharpCode(string source)
        {
            this.code = Pretty(source);
            this.Class = Regex.Match(source, @"(?<=class |enum |interface )\w+").Value;
        }

        /// <summary>
        /// 获取所有行
        /// </summary>
        public IEnumerable<string> Lines
        {
            get => GetLines(this.code);
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.code;
        }

        /// <summary>
        /// 美化
        /// </summary>
        /// <param name="code">源代码</param>
        /// <returns></returns>
        private static string Pretty(string code)
        {
            if (code == null)
            {
                return null;
            }

            var tab = 0;
            var builder = new StringBuilder();
            var compactCode = Compact(code);

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

                var isEndMethod = line.EndsWith(");");
                var isEndEnum = Regex.IsMatch(line, @"=\s*\d+\s*,");
                var isEndProperty = line.EndsWith("{ get; set; }");

                var prefix = string.Empty.PadRight(cTab * 4, ' ');
                var suffix = isEndMethod || isEndEnum || isEndProperty ?
                    Environment.NewLine :
                    null;

                builder.AppendLine($"{prefix}{line}{suffix}");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 使代码紧凑
        /// </summary>
        /// <param name="code">源代码</param>
        /// <returns></returns>
        private static string Compact(string code)
        {
            if (code == null)
            {
                return null;
            }

            var builder = new StringBuilder();
            foreach (var line in GetLines(code))
            {
                var spaceTrim = Regex.Replace(line.Trim(), @"(?<=\().*(?=\))", m => m.Value.Trim());
                builder.AppendLine(spaceTrim);
            }

            var rn = Environment.NewLine;
            var trimCode = builder.ToString().Trim();
            return Regex.Replace(trimCode, $@"{rn}\s*{rn}", rn);
        }

        /// <summary>
        /// 返回所有行
        /// </summary>
        /// <param name="code">源代码</param>
        /// <returns></returns>
        public static IEnumerable<string> GetLines(string code)
        {
            if (code == null)
            {
                yield break;
            }

            using (var reader = new StringReader(code))
            {
                while (reader.Peek() >= 0)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}
