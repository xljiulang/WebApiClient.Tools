using AngleSharp.Parser.Html;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示WebApiClient的接口数据模型
    /// </summary>
    [DebuggerDisplay("Interface = {Interface}")]
    public class HttpApi : CSharpControllerTemplateModel
    {
        /// <summary>
        /// 获取接口名称
        /// </summary>
        public string Interface { get; private set; }

        /// <summary>
        /// 获取文档描述
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// 获取是否有文档描述
        /// </summary>
        public bool HasSummary
        {
            get => string.IsNullOrEmpty(Summary) == false;
        }

        /// <summary>
        /// WebApiClient的接口数据模型
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="operations">swagger操作</param>
        /// <param name="document">swagger文档</param>
        /// <param name="settings">设置项</param>
        public HttpApi(string className, IEnumerable<CSharpOperationModel> operations, SwaggerDocument document, HttpApiSettings settings)
            : base(className, operations, document, settings)
        {
            var tag = document.Tags
                .FirstOrDefault(item => string.Equals(item.Name, className, StringComparison.OrdinalIgnoreCase));

            this.Interface = $"I{this.Class}Api";
            this.Summary = tag?.Description;
        }

        /// <summary>
        /// 转换为WebApiClient接口声明的c#代码
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
