using Microsoft.AspNetCore.Mvc;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Threading.Tasks;

namespace WebApiClient.Tools.Swagger.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var url = "https://iot.taichuan.net/swagger/docs/v1";
            var doc = await SwaggerDocument.FromUrlAsync(url);
            var swagger = new Swagger(doc);
            return View(swagger);
        }
    }
}
