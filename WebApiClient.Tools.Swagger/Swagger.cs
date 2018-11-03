using NJsonSchema;
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
    public class Swagger
    {
        public SwaggerDocument Document { get; private set; }

        public HttpApiSettings Settings { get; private set; }

        public Swagger(SwaggerDocument document)
        {
            this.Document = document;
            this.Settings = new HttpApiSettings();
        }

        public HttpApi[] GetHttpApis()
        {
            var generator = new Generator(this);
            return generator.GetHttpApiModels();
        }

        public HttpModel[] GetHttpModels()
        {
            var generator = new Generator(this);
            var codes = generator.GenerateModelCodes();
            return HttpModel.FromCodes(codes, this.Settings.AspNetNamespace);
        }

        public void GenerateFiles()
        {
            var path = this.Settings.AspNetNamespace.Split('.').Last();
            Directory.CreateDirectory(path);

            var apis = this.GetHttpApis();
            var models = this.GetHttpModels();

            foreach (var api in apis)
            {
                var file = Path.Combine(path, $"{api.Interface}.cs");
                Console.WriteLine($"输出文件：{file}");
                File.WriteAllText(file, api.ToString(), Encoding.UTF8);
            }

            foreach (var model in models)
            {
                var file = Path.Combine(path, $"{model.Class}.cs");
                Console.WriteLine($"输出文件：{file}");
                File.WriteAllText(file, model.ToString(), Encoding.UTF8);
            }
        }

        private class Generator : SwaggerToCSharpControllerGenerator
        {
            private readonly Swagger swagger;

            private readonly List<HttpApi> httpApiList = new List<HttpApi>();

            public Generator(Swagger swagger)
                : base(swagger.Document, swagger.Settings)
            {
                this.swagger = swagger;
            }

            public string GenerateModelCodes()
            {
                var generator = new CSharpGenerator(this.swagger.Document, this.swagger.Settings.CSharpGeneratorSettings, (CSharpTypeResolver)this.Resolver);
                var codes = generator.GenerateTypes().Concatenate();
                return TransformCode(codes);
            }

            public HttpApi[] GetHttpApiModels()
            {
                this.httpApiList.Clear();
                this.GenerateFile();
                return this.httpApiList.ToArray();
            }

            protected override string GenerateClientClass(string controllerName, string controllerClassName, IList<CSharpOperationModel> operations, ClientGeneratorOutputType outputType)
            {
                var model = new HttpApi(controllerClassName, operations, this.swagger.Document, this.swagger.Settings);
                this.httpApiList.Add(model);
                return string.Empty;
            }

            protected override string GenerateFile(string clientCode, IEnumerable<string> clientClasses, ClientGeneratorOutputType outputType)
            {
                return string.Empty;
            }

            protected override CSharpOperationModel CreateOperationModel(SwaggerOperation operation, ClientGeneratorBaseSettings settings)
            {
                return new HttpApiOperation(operation, (SwaggerToCSharpGeneratorSettings)settings, this, (CSharpTypeResolver)Resolver);
            }

            private static string TransformCode(string codes)
            {
                var builder = new StringBuilder();
                var lines = Code.GetLines(codes);

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
                    }
                    else
                    {
                        var @short = line
                            .Replace("System.ComponentModel.DataAnnotations.", null)
                            .Replace("System.Collections.Generic.", null);

                        builder.AppendLine(@short);
                    }
                }

                return builder.ToString();
            }
        }
    }
}