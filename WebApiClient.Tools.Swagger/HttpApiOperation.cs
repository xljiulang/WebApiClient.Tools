using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace WebApiClient.Tools.Swagger
{
    public class HttpApiOperation : CSharpOperationModel
    {
        public HttpApiOperation(SwaggerOperation operation, SwaggerToCSharpGeneratorSettings settings, SwaggerToCSharpGeneratorBase generator, CSharpTypeResolver resolver)
            : base(operation, settings, generator, resolver)
        {
        }

        public override string ResultType
        {
            get
            {
                var dataType = SyncResultType
                    .Replace("«", "<")
                    .Replace("»", ">");

                return dataType == "void"
                    ? "ITask<HttpResponseMessage>"
                    : $"ITask<{dataType}>";
            }
        }

        /// <summary>Resolves the type of the parameter.</summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The parameter type name.</returns>
        protected override string ResolveParameterType(SwaggerParameter parameter)
        {
            var schema = parameter.ActualSchema;
            if (schema.Type == JsonObjectType.File)
            {
                if (parameter.CollectionFormat == SwaggerParameterCollectionFormat.Multi && !schema.Type.HasFlag(JsonObjectType.Array))
                    return "MulitpartFile[]";

                return "MulitpartFile";
            }

            return base.ResolveParameterType(parameter);
        }
    }
}
