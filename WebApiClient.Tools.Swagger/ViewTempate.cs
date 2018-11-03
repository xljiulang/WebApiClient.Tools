using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;

namespace WebApiClient.Tools.Swagger
{
    /// <summary>
    /// 表示视图模板
    /// </summary>
    class ViewTempate : ITemplateSource
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
        /// 视图模板
        /// </summary>
        static ViewTempate()
        {
            var config = new TemplateServiceConfiguration
            {
                Debug = false
            };
            razor = RazorEngineService.Create(config);
        }

        /// <summary>
        /// 返回视图执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <returns></returns>
        public static string View<T>(T model)
        {
            return View<T>(model, typeof(T).Name);
        }

        /// <summary>
        /// 返回视图执行结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">模型</param>
        /// <param name="name">视图名称</param>
        /// <returns></returns>
        public static string View<T>(T model, string name)
        {
            lock (syncRoot)
            {
                if (hashSet.Add(name) == true)
                {
                    var view = new ViewTempate(name);
                    razor.AddTemplate(name, view);
                    razor.Compile(name);
                }
            }
            return razor.RunCompile(name, typeof(T), model);
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
        public ViewTempate(string name)
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
