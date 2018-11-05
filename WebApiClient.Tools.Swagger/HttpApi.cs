using NJsonSchema.CodeGeneration;
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
    [DebuggerDisplay("TypeName = {TypeName}")]
    public class HttpApi : CSharpControllerTemplateModel
    {
        /// <summary>
        /// 获取接口名称
        /// </summary>
        public string TypeName { get; private set; }

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

            this.TypeName = $"I{this.Class}Api";
            this.Summary = tag?.Description;
        }

        /// <summary>
        /// 转换为完整的代码
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var cshtml = CSharpHtml.Views(nameof(HttpApi));
            var source = cshtml.RenderText(this);
            return new CSharpCode(source, this.TypeName, CodeArtifactType.Interface).ToString();
        }
    }
}
