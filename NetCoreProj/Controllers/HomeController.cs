using Microsoft.AspNetCore.Mvc;

namespace NetCoreProj.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public string Index() => $"HomeController= {Request.Path}";
    }
}