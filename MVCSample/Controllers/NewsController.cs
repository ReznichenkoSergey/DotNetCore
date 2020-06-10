using Microsoft.AspNetCore.Mvc;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using System.Linq;

namespace MVCSample.Controllers
{
    public class NewsController : Controller
    {
        readonly INewsRepository Repository;
        public NewsController(INewsRepository repository)
        {
            Repository = repository;
        }

        //[Route("Index")]
        //[Route("[action]")]
        public IActionResult Show()
        {
            /*if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();*/
            //ViewData["NewsList"] = Repository.GetAllNew().ToList();
            var obj = Repository.GetAllNew().ToList();
            return View("Show", obj);
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Index(int id)
        {
            /*if (NewsBase.News.Any())
                ViewData["NewsList"] = NewsBase.News;
            else
                return new NotFoundResult();*/
            if(id!= 0)
                ViewData["NewsList"] = Repository.GetAllNew().Where(x=>x.Id == id).ToList();
            ViewData["NewsList"] = Repository.GetAllNew().ToList();
            return View();
        }
    }
}