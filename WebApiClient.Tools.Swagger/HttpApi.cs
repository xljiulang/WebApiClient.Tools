using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using RazorEngine;
using RazorEngine.Templating;
using System.Collections.Generic;

namespace WebApiClient.Tools.Swagger
{
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

        public override string ToString()
        {
            return Engine.Razor.RunCompile(view.ViewName, typeof(HttpApi), this);
        }
    }
}
