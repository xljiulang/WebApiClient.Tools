using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示Swagger描述
    /// </summary>
    public class Swagger
    {
        private readonly CSharpTypeResolver resolver;

        /// <summary>
        /// 获取Swagger文档
        /// </summary>
        public SwaggerDocument Document { get; private set; }

        /// <summary>
        /// 获取Swagger设置项
        /// </summary>
        public HttpApiSettings Settings { get; private set; }

        /// <summary>
        /// Swagger描述
        /// </summary>
        /// <param name="options">选项</param>
        public Swagger(SwaggerOptions options)
            : this(GetDocument(options.Swagger))
        {
            if (string.IsNullOrEmpty(options.Namespace) == false)
            {
                this.Settings.AspNetNamespace = options.Namespace;
            }
        }

        /// <summary>
        /// Swagger描述
        /// </summary>
        /// <param name="document">Swagger文档</param>
        public Swagger(SwaggerDocument document)
        {
            this.Document = PrettyName(document);
            this.Settings = new HttpApiSettings();

            this.resolver = SwaggerToCSharpGeneratorBase
                .CreateResolverWithExceptionSchema(this.Settings.CSharpGeneratorSettings, document);
        }

        /// <summary>
        /// 获取swagger文档
        /// </summary>
        /// <param name="swagger"></param>
        /// <returns></returns>
        private static SwaggerDocument GetDocument(string swagger)
        {
            Console.WriteLine($"正在分析swagger：{swagger}");
            if (Uri.TryCreate(swagger, UriKind.Absolute, out var _) == true)
            {
                return SwaggerDocument.FromUrlAsync(swagger).Result;
            }
            else
            {
                return SwaggerDocument.FromFileAsync(swagger).Result;
            }
        }

        /// <summary>
        /// 对文档的Definitions类型名称美化
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static SwaggerDocument PrettyName(SwaggerDocument document)
        {
            var changed = false;
            var json = document.ToJson();

            foreach (var item in document.Definitions)
            {
                var name = item.Key;
                var prettyName = PrettyName(name);

                if (name != prettyName)
                {
                    changed = true;
                    var escapeName = Regex.Escape(name);

                    json = Regex.Replace(json, $@"(?<=#/definitions/){escapeName}", prettyName);
                    json = Regex.Replace(json, $@"(?<=""){escapeName}(?="")", prettyName);
                }
            }

            return changed ? SwaggerDocument.FromJsonAsync(json).Result : document;
        }


        /// <summary>
        /// 美化名称
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private static string PrettyName(string name)
        {
            if (name.Contains("[]") == true)
            {
                name = name.Replace("[]", "Array");
            }

            var matchs = Regex.Matches(name, @"\W");
            if (matchs.Count == 0 || matchs.Count % 2 > 0)
            {
                return name;
            }

            var index = -1;
            return Regex.Replace(name, @"\W", m =>
            {
                index = index + 1;
                return index < matchs.Count / 2 ? "Of" : null;
            });
        }

        /// <summary>
        /// 获取所有HttpApi描述模型
        /// </summary>
        /// <returns></returns>
        public HttpApi[] GetHttpApis()
        {
            var provider = new HttpApiProvider(this);
            return provider.GetHttpApiModels();
        }

        /// <summary>
        /// 获取所有HttpModel描述模型
        /// </summary>
        /// <returns></returns>
        public HttpModel[] GetHttpModels()
        {
            var provider = new HttpModelProvider(this);
            return provider.GetHttpModels();
        }

        /// <summary>
        /// 生成代码并保存到文件
        /// </summary>
        public void GenerateFiles()
        {
            var dir = Path.Combine("output", this.Settings.AspNetNamespace);
            var apisPath = Path.Combine(dir, "HttpApis");
            var modelsPath = Path.Combine(dir, "HttpModels");

            Directory.CreateDirectory(apisPath);
            Directory.CreateDirectory(modelsPath);

            var apis = this.GetHttpApis();
            foreach (var api in apis)
            {
                var file = Path.Combine(apisPath, $"{api.Interface}.cs");
                File.WriteAllText(file, api.ToString(), Encoding.UTF8);
                Console.WriteLine($"输出接口文件：{file}");
            }

            var models = this.GetHttpModels();
            foreach (var model in models)
            {
                var file = Path.Combine(modelsPath, $"{model.Class}.cs");
                File.WriteAllText(file, model.ToString(), Encoding.UTF8);
                Console.WriteLine($"输出模型文件：{file}");
            }

            Console.WriteLine($"共输出{apis.Length + models.Length}个文件..");
        }

        /// <summary>
        /// 表示HttpApi提供者
        /// </summary>
        private class HttpApiProvider : SwaggerToCSharpControllerGenerator
        {
            private readonly Swagger swagger;

            private readonly List<HttpApi> httpApiList = new List<HttpApi>();

            /// <summary>
            /// HttpApi提供者
            /// </summary>
            /// <param name="swagger"></param>
            public HttpApiProvider(Swagger swagger)
                : base(swagger.Document, swagger.Settings, swagger.resolver)
            {
                this.swagger = swagger;
            }

            /// <summary>
            /// 获取所有HttpApi描述模型
            /// </summary>
            /// <returns></returns>
            public HttpApi[] GetHttpApiModels()
            {
                this.httpApiList.Clear();
                this.GenerateFile();
                return this.httpApiList.ToArray();
            }

            /// <summary>
            /// 生成客户端调用代码
            /// 但实际只为了获得HttpApi实例
            /// </summary>
            /// <param name="controllerName"></param>
            /// <param name="controllerClassName"></param>
            /// <param name="operations"></param>
            /// <param name="outputType"></param>
            /// <returns></returns>
            protected override string GenerateClientClass(string controllerName, string controllerClassName, IList<CSharpOperationModel> operations, ClientGeneratorOutputType outputType)
            {
                var model = new HttpApi(controllerClassName, operations, this.swagger.Document, this.swagger.Settings);
                this.httpApiList.Add(model);
                return string.Empty;
            }

            /// <summary>
            /// 生成文件
            /// 这里不生成
            /// </summary>
            /// <param name="clientCode"></param>
            /// <param name="clientClasses"></param>
            /// <param name="outputType"></param>
            /// <returns></returns>
            protected override string GenerateFile(string clientCode, IEnumerable<string> clientClasses, ClientGeneratorOutputType outputType)
            {
                return string.Empty;
            }

            /// <summary>
            /// 创建操作描述
            /// 这里创建HttpApiOperation
            /// </summary>
            /// <param name="operation"></param>
            /// <param name="settings"></param>
            /// <returns></returns>
            protected override CSharpOperationModel CreateOperationModel(SwaggerOperation operation, ClientGeneratorBaseSettings settings)
            {
                return new HttpApiMethod(operation, (SwaggerToCSharpGeneratorSettings)settings, this, (CSharpTypeResolver)Resolver);
            }
        }

        /// <summary>
        /// 表示HttpModel提供者
        /// </summary>
        private class HttpModelProvider : CSharpGenerator
        {
            private readonly Swagger swagger;

            /// <summary>
            /// HttpModel提供者
            /// </summary>
            /// <param name="swagger"></param>
            public HttpModelProvider(Swagger swagger)
                : base(swagger.Document, swagger.Settings.CSharpGeneratorSettings, swagger.resolver)
            {
                this.swagger = swagger;
            }

            /// <summary>
            /// 获取所有HttpModels
            /// </summary>
            /// <returns></returns>
            public HttpModel[] GetHttpModels()
            {
                return this.GenerateTypes()
                    .Artifacts
                    .Select(item => new HttpModel(item, this.swagger.Settings.AspNetNamespace))
                    .ToArray();
            }
        }
    }
}