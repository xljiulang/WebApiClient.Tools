using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using NSwag.CodeGeneration.OperationNameGenerators;
using System.Collections.Generic;
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
            this.OperationNameGenerator = new OperationNameProvider();
            this.ParameterNameGenerator = new ParameterNameGeProvider();
            this.CSharpGeneratorSettings.ClassStyle = CSharpClassStyle.Poco;
            this.CSharpGeneratorSettings.GenerateJsonMethods = false;
            this.RouteNamingStrategy = CSharpControllerRouteNamingStrategy.OperationId;
        }

        /// <summary>
        /// 方法名称提供者
        /// </summary>
        private class OperationNameProvider : MultipleClientsFromOperationIdOperationNameGenerator
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
                return operation.Tags.FirstOrDefault();
            }
        }

        /// <summary>
        /// 参数名提供者
        /// </summary>
        private class ParameterNameGeProvider : IParameterNameGenerator
        {
            /// <summary>
            /// 生成参数名
            /// </summary>
            /// <param name="parameter"></param>
            /// <param name="allParameters"></param>
            /// <returns></returns>
            public string Generate(SwaggerParameter parameter, IEnumerable<SwaggerParameter> allParameters)
            {
                if (string.IsNullOrEmpty(parameter.Name))
                {
                    return "unnamed";
                }

                var variableName = CamelCase(parameter.Name
                    .Replace("-", "_")
                    .Replace(".", "_")
                    .Replace("$", string.Empty)
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty));

                if (allParameters.Count(p => p.Name == parameter.Name) > 1)
                    return variableName + parameter.Kind;

                return variableName;
            }

            /// <summary>
            /// 骆驼命名
            /// </summary>
            /// <param name="name">名称</param>
            /// <returns></returns>
            private static string CamelCase(string name)
            {
                if (string.IsNullOrEmpty(name) || char.IsUpper(name[0]) == false)
                {
                    return name;
                }

                var charArray = name.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    if (i == 1 && char.IsUpper(charArray[i]) == false)
                    {
                        break;
                    }

                    var hasNext = (i + 1 < charArray.Length);
                    if (i > 0 && hasNext && !char.IsUpper(charArray[i + 1]))
                    {
                        if (char.IsSeparator(charArray[i + 1]))
                        {
                            charArray[i] = char.ToLowerInvariant(charArray[i]);
                        }
                        break;
                    }
                    charArray[i] = char.ToLowerInvariant(charArray[i]);
                }
                return new string(charArray);
            }
        }
    }
}
