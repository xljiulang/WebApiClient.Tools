using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using NSwag.CodeGeneration.OperationNameGenerators;
using System.Linq;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示WebApiClient接口设置模型
    /// </summary>
    public class HttpApiSettings : SwaggerToCSharpControllerGeneratorSettings
    {
        /// <summary>
        /// WebApiClient接口设置模型
        /// </summary>
        public HttpApiSettings()
        {
            this.ResponseArrayType = "List";
            this.ResponseDictionaryType = "Dictionary";
            this.ParameterArrayType = "IEnumerable";
            this.ParameterDictionaryType = "IDictionary";

            this.AspNetNamespace = this.GetType().Namespace;
            this.OperationNameGenerator = new OperationNameGenerators();
            this.CSharpGeneratorSettings.ClassStyle = CSharpClassStyle.Poco;
            this.CSharpGeneratorSettings.GenerateJsonMethods = false;
            this.RouteNamingStrategy = CSharpControllerRouteNamingStrategy.OperationId;
        }

        /// <summary>
        /// 方法名称提示者
        /// </summary>
        private class OperationNameGenerators : MultipleClientsFromOperationIdOperationNameGenerator
        {
            /// <summary>
            /// 获取方法对应的类名
            /// </summary>
            /// <param name="document"></param>
            /// <param name="path"></param>
            /// <param name="httpMethod"></param>
            /// <param name="operation"></param>
            /// <returns></returns>
            public override string GetClientName(SwaggerDocument document, string path, SwaggerOperationMethod httpMethod, SwaggerOperation operation)
            {
                var name = base.GetClientName(document, path, httpMethod, operation);

                // 取不到名称，就用第一个Tag做为类名
                if (string.IsNullOrEmpty(name) == true)
                {
                    return operation.Tags.FirstOrDefault();
                }
                return name;
            }
        }
    }
}
