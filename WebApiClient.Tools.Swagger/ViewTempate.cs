using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Collections.Generic;
using System.IO;

namespace WebApiClient.Tools.Swagger
{
    public class ViewTempate : ITemplateSource
    {
        private static readonly IRazorEngineService razor;

        private static readonly object syncRoot = new object();

        private static readonly HashSet<string> hashSet = new HashSet<string>();

        static ViewTempate()
        {
            var config = new TemplateServiceConfiguration
            {
                Debug = false
            };
            razor = RazorEngineService.Create(config);
        }

        public static string View<T>(T model)
        {
            return View<T>(model, typeof(T).Name);
        }

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

        private string cshtml;

        public string Name { get; private set; }

        public ViewTempate(string viewName)
        {
            this.Name = viewName;
        }

        public string TemplateFile
        {
            get => null;
        }

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

        public TextReader GetTemplateReader()
        {
            return new StringReader(this.Template);
        }
    }
}
