using AngleSharp.Parser.Html;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Class = {Class}")]
    public class HttpApi : CSharpControllerTemplateModel
    {
        static readonly ViewTempate view = new ViewTempate("HttpApi");

        static HttpApi()
        {
            Engine.Razor.AddTemplate(view.ViewName, view);
        }

        public HttpApi(string controllerName, IEnumerable<CSharpOperationModel> operations, SwaggerDocument document, HttpApiSettings settings)
            : base(controllerName, operations, document, settings)
        {
        }

        public string ToHtmlString()
        {
            return Engine.Razor.RunCompile(view.ViewName, typeof(HttpApi), this);
        }

        public override string ToString()
        {
            var html = this.ToHtmlString();
            var document = new HtmlParser().Parse(html);
            return document.Body.InnerText.Replace("\n \n", "\n");
        }
    }
}
