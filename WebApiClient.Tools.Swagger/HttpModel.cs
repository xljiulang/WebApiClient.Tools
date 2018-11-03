using AngleSharp.Parser.Html;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
        private HttpModel(string code, string nameSpace)
            : base(code)
        {
            this.AspNetNamespace = nameSpace;
        }

        /// <summary>
        /// 从所有模型代码里分离出多个HttpModel
        /// </summary>
        /// <param name="codes">所有代码</param>
        /// <param name="nameSpace">使用的命名空间</param>
        /// <returns></returns>
        public static HttpModel[] FromCodes(string codes, string nameSpace)
        {
            var builder = new StringBuilder();
            var httpModels = new List<HttpModel>();

            foreach (var line in CSharpCode.GetLines(codes))
            {
                builder.AppendLine(line);
                if (string.Equals(line, "}") == true)
                {
                    var model = new HttpModel(builder.ToString(), nameSpace);
                    httpModels.Add(model);
                    builder.Clear();
                }
            }
            return httpModels.ToArray();
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
