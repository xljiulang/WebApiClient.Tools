using CommandLine;
using NSwag;
using System;

namespace WebApiClient.Tools.Swagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new SwaggerOptions();
            if (Parser.Default.ParseArguments(args, options))
            {
                var doc = Uri.TryCreate(options.Swagger, UriKind.Absolute, out var _) ?
                    SwaggerDocument.FromUrlAsync(options.Swagger).Result :
                    SwaggerDocument.FromFileAsync(options.Swagger).Result;

                var swagger = new Swagger(doc);
                if (string.IsNullOrEmpty(options.Namespace) == false)
                {
                    swagger.Settings.AspNetNamespace = options.Namespace;
                }
                swagger.GenerateFiles();
            }
            else
            {
                Console.WriteLine(options.GetUsage());
                Console.Read();
            }
        }
    }
}
