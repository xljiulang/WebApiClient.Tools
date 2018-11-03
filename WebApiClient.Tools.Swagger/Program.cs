using NSwag;
using System;
using System.IO;
namespace WebApiClient.Tools.Swagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("api-docs.json");
            var doc = SwaggerDocument.FromJsonAsync(json).Result;
            var swagger = new Swagger(doc);

            swagger.GenerateFiles();

            Console.ReadLine();
        }
    }
}
