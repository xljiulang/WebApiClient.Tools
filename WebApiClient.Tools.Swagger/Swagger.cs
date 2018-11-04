using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示Swagger描述
    /// </summary>
    public class Swagger
    {
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
        /// 获取swagger文档
        /// </summary>
        /// <param name="swagger"></param>
        /// <returns></returns>
        static SwaggerDocument GetDocument(string swagger)
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
        /// Swagger描述
        /// </summary>
        /// <param name="document">Swagger文档</param>
        public Swagger(SwaggerDocument document)
        {
            this.Document = document;
            this.Settings = new HttpApiSettings();
        }

        /// <summary>
        /// 获取所有HttpApi描述模型
        /// </summary>
        /// <returns></returns>
        public HttpApi[] GetHttpApis()
        {
            var generator = new Generator(this);
            return generator.GetHttpApiModels();
        }

        /// <summary>
        /// 获取所有HttpModel描述模型
        /// </summary>
        /// <returns></returns>
        public HttpModel[] GetHttpModels()
        {
            var generator = new Generator(this);
            var codes = generator.GenerateModelCodes();
            return HttpModel.FromCodes(codes, this.Settings.AspNetNamespace);
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
        /// 表示代码生成器
        /// </summary>
        private class Generator : SwaggerToCSharpControllerGenerator
        {
            private readonly Swagger swagger;

            private readonly List<HttpApi> httpApiList = new List<HttpApi>();

            /// <summary>
            /// 代码生成器
            /// </summary>
            /// <param name="swagger"></param>
            public Generator(Swagger swagger)
                : base(swagger.Document, swagger.Settings)
            {
                this.swagger = swagger;
            }

            /// <summary>
            /// 生成模型的代码
            /// 这里是NSwag已经实现
            /// </summary>
            /// <returns></returns>
            public string GenerateModelCodes()
            {
                var generator = new CSharpGenerator(this.swagger.Document, this.swagger.Settings.CSharpGeneratorSettings, (CSharpTypeResolver)this.Resolver);
                var nswagCodes = generator.GenerateTypes().Concatenate();
                return TransformCode(nswagCodes);
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

            /// <summary>
            /// 转换代码
            /// 将NSwag生成的模型代码转换为WebApiClient的模型代码
            /// </summary>
            /// <param name="nswagCodes"></param>
            /// <returns></returns>
            private static string TransformCode(string nswagCodes)
            {
                var builder = new StringBuilder();
                var lines = CSharpCode.GetLines(nswagCodes);

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
                        .Replace("«", "<")
                        .Replace("»", ">")
                        .Replace("System.Collections.Generic.", null)
                        .Replace("System.ComponentModel.DataAnnotations.", null);

                    builder.AppendLine(cleaned);
                }

                return builder.ToString();
            }
        }
    }
}