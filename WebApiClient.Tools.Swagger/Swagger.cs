using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using System.Collections.Generic;
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
            return HttpModel.FromCodes(codes);
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
                return new CodeCleaner(codes).ToString();
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
        }

        private class CodeCleaner
        {
            private readonly StringBuilder builder = new StringBuilder();

            public CodeCleaner(string codes)
            {
                foreach (var line in codes.Split('\n'))
                {
                    var clear = this.Clear(line);
                    if (clear != null)
                    {
                        builder.AppendLine(clear);
                    }
                }
            }

            private string Clear(string line)
            {
                if (line.Contains("System.CodeDom.Compiler.GeneratedCode"))
                {
                    return null;
                }

                var regex = new Regex("(?<=Newtonsoft.Json.JsonProperty\\(\")\\w+(?=\")");
                var match = regex.Match(line);
                if (match.Success == true)
                {
                    return $"    [AliasAs(\"{match.Value}\")]";
                }
                else
                {
                    line = line.Replace("System.ComponentModel.DataAnnotations.", null);
                }
                return line;
            }

            public override string ToString()
            {
                return this.builder.ToString();
            }
        }
    }
}