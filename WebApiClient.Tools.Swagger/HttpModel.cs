using AngleSharp.Parser.Html;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Class = {Class}")]
    public class HttpModel : Code
    {
        public string AspNetNamespace { get; private set; }

        private HttpModel(string code, string nameSpace)
            : base(code)
        {
            this.AspNetNamespace = nameSpace;
        }

        public static HttpModel[] FromCodes(string codes, string nameSpace)
        {
            var builder = new StringBuilder();
            var list = new List<HttpModel>();

            foreach (var str in Code.GetLines(codes))
            {
                builder.AppendLine(str.Replace("«", "<").Replace("»", ">"));
                if (string.Equals(str, "}") == true)
                {
                    list.Add(new HttpModel(builder.ToString(), nameSpace));
                    builder.Clear();
                }
            }
            return list.ToArray();
        }

        public override string ToString()
        {
            var html = ViewTempate.View(this);
            var source = new HtmlParser().Parse(html).Body.InnerText;
            return new Code(source).ToString();
        }
    }
}
