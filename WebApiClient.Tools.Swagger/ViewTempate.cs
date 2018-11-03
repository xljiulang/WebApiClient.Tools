using RazorEngine.Templating;
using System.IO;

namespace WebApiClient.Tools.Swagger
{
    public class ViewTempate : ITemplateSource
    {
        private string cshtml;

        public string ViewName { get; private set; }

        public ViewTempate(string viewName)
        {
            this.ViewName = viewName;
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
                    var name = $"{this.GetType().Namespace}.Views.{this.ViewName}.cshtml";
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
