using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示视图模板
    /// </summary>
    [DebuggerDisplay("{Path}")]
    class CSharpHtml : ITemplateSource
    {
        /// <summary>
        /// razor引擎
        /// </summary>
        private static readonly IRazorEngineService razor;

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object syncRoot = new object();

        /// <summary>
        /// 视图名称集合
        /// </summary>
        private static readonly HashSet<string> templateNames = new HashSet<string>();

        /// <summary>
        /// 块元素
        /// </summary>
        private static readonly HashSet<string> blockElements = new HashSet<string>(new[] { "div", "p" }, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 视图模板
        /// </summary>
        static CSharpHtml()
        {
            var config = new TemplateServiceConfiguration
            {
                Debug = false,
                CachingProvider = new DefaultCachingProvider(t => { })
            };
            razor = RazorEngineService.Create(config);
        }

        /// <summary>
        /// 返回Views下的cshtml
        /// </summary>
        /// <param name="name">cshtml名称</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static CSharpHtml Views(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var path = $"Views.{name}";
            return new CSharpHtml(path);
        }


        /// <summary>
        /// 模板内容
        /// </summary>
        private readonly Lazy<string> template;

        /// <summary>
        /// 获取cshtml资源路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 获取模板内容
        /// </summary>
        public string Template
        {
            get => this.template.Value;
        }

        /// <summary>
        /// 获取模板文件路径
        /// </summary>
        public string TemplateFile
        {
            get => null;
        }

        /// <summary>
        /// 视图模板
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CSharpHtml(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase) == false)
            {
                path = $"{path}.cshtml";
            }

            this.Path = path;
            this.template = new Lazy<string>(this.ReadTemplate);
        }

        /// <summary>
        /// 读取模板资源
        /// </summary>
        /// <returns></returns>
        private string ReadTemplate()
        {
            var fullPath = $"{typeof(CSharpHtml).Namespace}.{this.Path}";
            using (var stream = typeof(CSharpHtml).Assembly.GetManifestResourceStream(fullPath))
            {
                return new StreamReader(stream).ReadToEnd();
            }
        }

        /// <summary>
        /// 返回模板内容读取器
        /// </summary>
        /// <returns></returns>
        TextReader ITemplateSource.GetTemplateReader()
        {
            return new StringReader(this.Template);
        }

        /// <summary>
        /// 返回视图Html
        /// </summary>
        /// <param name="model">模型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public string RenderHtml(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            lock (syncRoot)
            {
                if (templateNames.Add(this.Path) == true)
                {
                    razor.AddTemplate(this.Path, this);
                    razor.Compile(this.Path);
                }
            }
            return razor.RunCompile(this.Path, model.GetType(), model);
        }

        /// <summary>
        /// 返回视图文本
        /// </summary>
        /// <param name="model">模型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public string RenderText(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var html = this.RenderHtml(model);
            var doc = XDocument.Parse(html).Root;
            var builder = new StringBuilder();

            RenderText(doc, builder);
            return builder.ToString();
        }


        /// <summary>
        /// 装载元素的文本
        /// </summary>
        /// <param name="element"></param>
        /// <param name="builder"></param>
        private static void RenderText(XElement element, StringBuilder builder)
        {
            if (element.HasElements == true)
            {
                foreach (var item in element.Elements())
                {
                    RenderText(item, builder);
                }
                return;
            }

            var text = element.Value?.Trim();
            if (string.IsNullOrEmpty(text) == true)
            {
                return;
            }

            if (blockElements.Contains(element.Name.ToString()))
            {
                builder.AppendLine().Append(text);
                if (element.NextNode == null)
                {
                    builder.AppendLine();
                }
            }
            else
            {
                builder.Append(text);
                if (element.NextNode != null)
                {
                    builder.Append(" ");
                }
            }
        }
    }
}
