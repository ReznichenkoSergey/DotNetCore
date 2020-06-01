using Microsoft.AspNetCore.Mvc;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using System.Linq;

namespace MVCSample.Controllers
{
    public class NewsController : Controller
    {
        INewsRepository Repository;
        public NewsController(INewsRepository repository)
        {
            Repository = repository;
        }

        public IActionResult Index()
        {
            /*if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();*/
            ViewData["NewsList"] = Repository.GetAllNew().ToList();
            return View();
        }
    }
}