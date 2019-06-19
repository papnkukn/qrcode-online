using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace QRCode.Controllers
{
    public class MainController : Controller
    {
        private readonly IHostingEnvironment env;

        public MainController(IHostingEnvironment env)
        {
            this.env = env;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = System.IO.File.ReadAllText(Path.Combine(env.WebRootPath, "index.html"))
            };
        }

        [HttpGet]
        [Route("webcam")]
        public IActionResult Webcam()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = System.IO.File.ReadAllText(Path.Combine(env.WebRootPath, "webcam.html"))
            };
        }
    }
}
