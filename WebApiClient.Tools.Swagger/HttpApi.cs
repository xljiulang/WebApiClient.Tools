using AngleSharp.Parser.Html;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Interface = {Interface}")]
    public class HttpApi : CSharpControllerTemplateModel
    {
        public string Interface { get; private set; }

        public HttpApi(string controllerName, IEnumerable<CSharpOperationModel> operations, SwaggerDocument document, HttpApiSettings settings)
            : base(controllerName, operations, document, settings)
        {
            this.Interface = $"I{this.Class}Api";
        }

        public override string ToString()
        {
            var html = ViewTempate.View(this);
            var source = new HtmlParser().Parse(html).Body.InnerText;
            return new Code(source).ToString();
        }
    }
}
