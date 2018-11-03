using AngleSharp.Parser.Html;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Interface = {Interface}")]
    public class HttpApi : CSharpControllerTemplateModel
    {
        private static readonly ViewTempate view = new ViewTempate("HttpApi");

        public string Interface { get; private set; }

        static HttpApi()
        {
            Engine.Razor.AddTemplate(view.ViewName, view);
        }

        public HttpApi(string controllerName, IEnumerable<CSharpOperationModel> operations, SwaggerDocument document, HttpApiSettings settings)
            : base(controllerName, operations, document, settings)
        {
            this.Interface = $"I{this.Class}Api";
        }

        public override string ToString()
        {
            var html = Engine.Razor.RunCompile(view.ViewName, this.GetType(), this);
            var document = new HtmlParser().Parse(html);
            return document.Body.InnerText.Replace("\n \n", "\n");
        }
    }
}
