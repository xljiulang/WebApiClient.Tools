using NSwag;
using System;
using System.IO;
namespace WebApiClient.Tools.Swagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("v1.json");
            var doc = SwaggerDocument.FromJsonAsync(json).Result;
            var swagger = new Swagger(doc);
            var apis = swagger.GetHttpApis();
            var cshtml = apis[0].ToString();

            Console.WriteLine(cshtml);
            Console.ReadLine();
        }
    }
}
