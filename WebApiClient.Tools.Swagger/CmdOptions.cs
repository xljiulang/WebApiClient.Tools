using CommandLine;
using CommandLine.Text;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示命令选项
    /// </summary>
    class CmdOptions
    {
        /// <summary>
        /// swagger的json本地文件路径或远程Uri地址
        /// </summary>
        [Option('s', "swagger", MetaValue = "Swagger", Required = true, HelpText = "swagger的json本地文件路径或远程Uri地址")]
        public string Swagger { get; set; }

        /// <summary>
        /// 代码的命名空间
        /// </summary>
        [Option('n', "namespace", MetaValue = "Namespace", Required = false, HelpText = "代码的命名空间，如WebApiClient.Swagger")]
        public string Namespace { get; set; }

        /// <summary>
        /// 返回使用帮助
        /// </summary>
        /// <returns></returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
