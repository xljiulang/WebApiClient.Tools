using NJsonSchema.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace WebApiClient.Tools.Swagger
{
    public class HttpApiSettings : SwaggerToCSharpControllerGeneratorSettings
    {
        public HttpApiSettings()
        {
            this.AspNetNamespace = "WebApiClient";            
            this.CSharpGeneratorSettings.ClassStyle = CSharpClassStyle.Poco;
            this.CSharpGeneratorSettings.GenerateJsonMethods = false;
            this.RouteNamingStrategy = CSharpControllerRouteNamingStrategy.OperationId;
        }
    }
}
