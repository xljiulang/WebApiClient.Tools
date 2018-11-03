using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;
using NSwag.CodeGeneration.OperationNameGenerators;
using System.Linq;

namespace WebApiClient.Tools.Swagger
{
    public class HttpApiSettings : SwaggerToCSharpControllerGeneratorSettings
    {
        public HttpApiSettings()
        {
            this.AspNetNamespace = this.GetType().Namespace;
            this.OperationNameGenerator = new OperationNameGenerators();
            this.CSharpGeneratorSettings.ClassStyle = CSharpClassStyle.Poco;
            this.CSharpGeneratorSettings.GenerateJsonMethods = false;
            this.RouteNamingStrategy = CSharpControllerRouteNamingStrategy.OperationId;
        }

        private class OperationNameGenerators : MultipleClientsFromOperationIdOperationNameGenerator
        {
            public override string GetClientName(SwaggerDocument document, string path, SwaggerOperationMethod httpMethod, SwaggerOperation operation)
            {
                var name = base.GetClientName(document, path, httpMethod, operation);
                if (string.IsNullOrEmpty(name) == true)
                {
                    return operation.Tags.FirstOrDefault();
                }
                return name;
            }
        }
    }
}
