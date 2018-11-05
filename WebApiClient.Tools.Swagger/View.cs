using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示视图模板
    /// </summary>
    class View : ITemplateSource
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
        private static readonly HashSet<string> hashSet = new HashSet<string>();

        /// <summary>
        /// 块元素
        /// </summary>
        private static readonly HashSet<string> blockElements = new HashSet<string>(new[] { "div", "p" }, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 视图模板
        /// </summary>
        static View()
        {
            var config = new TemplateServiceConfiguration
            {
                Debug = false,
                CachingProvider = new DefaultCachingProvider(t => { })
            };
            razor = RazorEngineService.Create(config);
        }

        /// <summary>
        /// 返回视图文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public static string Text<T>(T model)
        {
            return Text(model, typeof(T).Name);
        }

        /// <summary>
        /// 返回视图文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <param name="name">视图名称</param>
        /// <returns></returns>
        public static string Text<T>(T model, string name)
        {
            var html = Html(model, name);
            var doc = XDocument.Parse(html).Root;
            var builder = new StringBuilder();
            Render(doc, builder);
            return builder.ToString();
        }


        /// <summary>
        /// 返回视图Html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public static string Html<T>(T model)
        {
            return Html(model, typeof(T).Name);
        }

        /// <summary>
        /// 返回视图Html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <param name="name">视图名称</param>
        /// <returns></returns>
        public static string Html<T>(T model, string name)
        {
            lock (syncRoot)
            {
                if (hashSet.Add(name) == true)
                {
                    var view = new View(name);
                    razor.AddTemplate(name, view);
                    razor.Compile(name);
                }
            }
            return razor.RunCompile(name, typeof(T), model);
        }


        /// <summary>
        /// 装载元素的文本
        /// </summary>
        /// <param name="element"></param>
        /// <param name="builder"></param>
        private static void Render(XElement element, StringBuilder builder)
        {
            if (element.HasElements == true)
            {
                foreach (var item in element.Elements())
                {
                    Render(item, builder);
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

        /// <summary>
        /// cshtml代码
        /// </summary>
        private string cshtml;

        /// <summary>
        /// 获取视图名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 视图模板
        /// </summary>
        /// <param name="name">视图名称</param>
        public View(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 模板文件，调试用
        /// </summary>
        string ITemplateSource.TemplateFile
        {
            get => null;
        }

        /// <summary>
        /// 获取模板内容
        /// </summary>
        public string Template
        {
            get
            {
                if (this.cshtml == null)
                {
                    var name = $"{this.GetType().Namespace}.Views.{this.Name}.cshtml";
                    using (var stream = this.GetType().Assembly.GetManifestResourceStream(name))
                    {
                        this.cshtml = new StreamReader(stream).ReadToEnd();
                    }
                }
                return this.cshtml;
            }
        }

        /// <summary>
        /// 返回内容读取器
        /// </summary>
        /// <returns></returns>
        public TextReader GetTemplateReader()
        {
            return new StringReader(this.Template);
        }
    }
}
