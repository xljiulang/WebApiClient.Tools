using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示WebApiClient的请求方法数据模型
    /// </summary>
    public class HttpApiOperation : CSharpOperationModel
    {
        /// <summary>
        /// WebApiClient的请求方法数据模型
        /// </summary>
        /// <param name="operation">Swagger操作</param>
        /// <param name="settings">设置项</param>
        /// <param name="generator">代码生成器</param>
        /// <param name="resolver">语法解析器</param>
        public HttpApiOperation(SwaggerOperation operation, SwaggerToCSharpGeneratorSettings settings, SwaggerToCSharpGeneratorBase generator, CSharpTypeResolver resolver)
            : base(operation, settings, generator, resolver)
        {
        }

        /// <summary>
        /// 获取方法的返回类型
        /// 默认使用ITask
        /// </summary>
        public override string ResultType
        {
            get
            {
                var dataType = SyncResultType
                    .Replace("«", "<")
                    .Replace("»", ">");

                return dataType == "void"
                    ? "ITask<HttpResponseMessage>"
                    : $"ITask<{dataType}>";
            }
        }

        /// <summary>
        /// 解析参数名称
        /// 将文件参数声明为MulitpartFile
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        protected override string ResolveParameterType(SwaggerParameter parameter)
        {
            var schema = parameter.ActualSchema;
            if (schema.Type == JsonObjectType.File)
            {
                if (parameter.CollectionFormat == SwaggerParameterCollectionFormat.Multi && !schema.Type.HasFlag(JsonObjectType.Array))
                {
                    return "IEnumerable<MulitpartFile>";
                }
                return "MulitpartFile";
            }

            return base.ResolveParameterType(parameter);
        }
    }
}
