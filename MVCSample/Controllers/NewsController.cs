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

        [Route("Index")]
        [Route("[action]")]
        public IActionResult Index()
        {
            /*if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();*/
            ViewData["NewsList"] = Repository.GetAllNew().ToList();
            return View();
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Index(int id)
        {
            /*if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();*/
            ViewData["NewsList"] = Repository.GetAllNew().Take(2).ToList();
            return View("Index");
        }
    }
}