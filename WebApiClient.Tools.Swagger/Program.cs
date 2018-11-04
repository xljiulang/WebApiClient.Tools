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
                var swagger = new Swagger(options);
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
