using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    public class Swagger
    {
        public SwaggerDocument Document { get; private set; }

        public SwaggerToCSharpControllerGeneratorSettings Settings { get; private set; }

        public Swagger(SwaggerDocument document)
        {
            this.Document = document;
            this.Settings = new SwaggerToCSharpControllerGeneratorSettings();
            this.Settings.CSharpGeneratorSettings.ClassStyle = CSharpClassStyle.Poco;
            this.Settings.CSharpGeneratorSettings.GenerateJsonMethods = false;
            this.Settings.RouteNamingStrategy = CSharpControllerRouteNamingStrategy.OperationId;
        }

        public CSharpControllerTemplateModel[] GetControllers()
        {
            var g = new Generator(this.Document, this.Settings);
            return g.GetCSharpControllerTemplateModels();
        }

        public string GetModelsCode()
        {
            var g = new Generator(this.Document, this.Settings);
            return g.GetDtoModelsCode();
        }


        private class Generator : SwaggerToCSharpControllerGenerator
        {
            private readonly SwaggerDocument document;
            private readonly SwaggerToCSharpControllerGeneratorSettings setting;

            public List<CSharpControllerTemplateModel> ControllerTemplateModels { get; private set; }

            public Generator(SwaggerDocument document, SwaggerToCSharpControllerGeneratorSettings setting)
                : base(document, setting)
            {
                this.document = document;
                this.setting = setting;
                this.ControllerTemplateModels = new List<CSharpControllerTemplateModel>();
            }

            public string GetDtoModelsCode()
            {
                var generator = new CSharpGenerator(this.document, this.setting.CSharpGeneratorSettings, (CSharpTypeResolver)this.Resolver);
                var codes = generator.GenerateTypes().Concatenate();
                return new WebApiClientModels(codes).ToString();
            }

            public CSharpControllerTemplateModel[] GetCSharpControllerTemplateModels()
            {
                this.GenerateFile();
                return this.ControllerTemplateModels.ToArray();
            }

            protected override string GenerateClientClass(string controllerName, string controllerClassName, IList<CSharpOperationModel> operations, ClientGeneratorOutputType outputType)
            {
                var model = new CSharpControllerTemplateModel(controllerClassName, operations, this.document, Settings);
                this.ControllerTemplateModels.Add(model);
                return string.Empty;
            }

            protected override string GenerateFile(string clientCode, IEnumerable<string> clientClasses, ClientGeneratorOutputType outputType)
            {
                return string.Empty;
            }

            protected override CSharpOperationModel CreateOperationModel(SwaggerOperation operation, ClientGeneratorBaseSettings settings)
            {
                return new WebApiClientOperationModel(operation, (SwaggerToCSharpGeneratorSettings)settings, this, (CSharpTypeResolver)Resolver);
            }
        }

        private class WebApiClientOperationModel : CSharpOperationModel
        {
            public WebApiClientOperationModel(SwaggerOperation operation, SwaggerToCSharpGeneratorSettings settings, SwaggerToCSharpGeneratorBase generator, CSharpTypeResolver resolver)
                : base(operation, settings, generator, resolver)
            {
            }

            public override string ResultType
            {
                get
                {
                    return SyncResultType == "void"
                        ? "ITask<HttpResponseMessage>"
                        : $"ITask<{SyncResultType}>";
                }
            }
        }

        private class WebApiClientModels
        {
            private readonly StringBuilder builder = new StringBuilder();

            public WebApiClientModels(string codes)
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