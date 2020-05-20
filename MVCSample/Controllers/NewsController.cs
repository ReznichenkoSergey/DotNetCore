using Microsoft.AspNetCore.Mvc;
using MVCSample.Models;
using System.Linq;

namespace MVCSample.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();
            return View();
        }
    }
}