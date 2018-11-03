using CommandLine;
using CommandLine.Text;

namespace WebApiClient.Tools.Swagger
{
    class SwaggerOptions
    {
        [Option('s', "swagger", MetaValue = "Swagger", Required = true, HelpText = "swagger的json本地文件路径或远程Uri地址")]
        public string Swagger { get; set; }

        [Option('n', "namespace", MetaValue = "Namespace", Required = false, HelpText = "代码的命名空间，如WebApiClient.Swagger")]
        public string Namespace { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
