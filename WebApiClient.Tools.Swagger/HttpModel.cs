using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiClient.Tools.Swagger
{
    [DebuggerDisplay("Class = {Class}")]
    public class HttpModel
    {
        private readonly string code;

        public string Class { get; private set; }

        private HttpModel(string code)
        {
            this.code = code;
            this.Class = Regex.Match(code, @"(?<=class )\w+").Value;
        }

        public static HttpModel[] FromCodes(string codes)
        {
            var builder = new StringBuilder();
            var list = new List<HttpModel>();
            var reader = new StringReader(codes);

            while (reader.Peek() >= 0)
            {
                var str = reader.ReadLine();
                builder.AppendLine(str);

                if (string.Equals(str, "}") == true)
                {
                    list.Add(new HttpModel(builder.ToString()));
                    builder.Clear();
                }
            }
            return list.ToArray();
        }

        public string ToHtmlString()
        {
            var builder = new StringBuilder();
            var reader = new StringReader(this.code);
            while (reader.Peek() >= 0)
            {
                var str = reader.ReadLine();
                var html = System.Web.HttpUtility.HtmlEncode(str);
                builder.AppendLine($"<div>{html}</div>");
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.code;
        }
    }
}
