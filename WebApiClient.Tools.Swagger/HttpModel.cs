using AngleSharp.Parser.Html;
using NJsonSchema.CodeGeneration;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示WebApiClient的模型描述
    /// </summary>
    [DebuggerDisplay("Class = {Class}")]
    public class HttpModel : CSharpCode
    {
        /// <summary>
        /// 获取使用的命名空间
        /// </summary>
        public string AspNetNamespace { get; private set; }

        /// <summary>
        /// WebApiClient的模型描述
        /// </summary>
        /// <param name="code">源代码</param>
        /// <param name="nameSpace">命名空间</param>
        public HttpModel(CodeArtifact code, string nameSpace)
           : base(TransformCode(code.Code))
        {
            this.AspNetNamespace = nameSpace;
        }
        /// <summary>
        /// 转换代码
        /// 将NSwag生成的模型代码转换为WebApiClient的模型代码
        /// </summary>
        /// <param name="nswagCode"></param>
        /// <returns></returns>
        private static string TransformCode(string nswagCode)
        {
            var builder = new StringBuilder();
            var lines = CSharpCode.GetLines(nswagCode);

            foreach (var line in lines)
            {
                if (line.Contains("System.CodeDom.Compiler.GeneratedCode"))
                {
                    continue;
                }

                var match = new Regex("(?<=Newtonsoft.Json.JsonProperty\\(\")\\w+(?=\")").Match(line);
                if (match.Success == true)
                {
                    builder.AppendLine($"[AliasAs(\"{match.Value}\")]");
                    continue;
                }

                var cleaned = line
                    .Replace("partial class", "class")
                    .Replace("System.Collections.Generic.", null)
                    .Replace("System.ComponentModel.DataAnnotations.", null);

                builder.AppendLine(cleaned);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 转换为WebApiClient模型声明的c#代码
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var html = ViewTempate.View(this);
            var source = new HtmlParser().Parse(html).Body.InnerText;
            return new CSharpCode(source).ToString();
        }
    }
}
