using Microsoft.AspNetCore.Mvc;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using System.Collections.Generic;
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

        public IActionResult Index(int? newsid)
        {
            List<News> list = null;
            if (newsid.HasValue)
                list = Repository
                    .GetAllNew()
                    .Where(x => x.Id == newsid)
                    .ToList();
            else
                list = Repository.GetAllNew().ToList();
            return View(list);
        }

        public IActionResult NewsByAuthor(int authorid)
        {
            var list = Repository
                .GetAllNew()
                .Where(x => x.AuthorId == authorid)
                .ToList();
            return View("Index", list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News news)
        {
            Repository.CreateNews(news);
            return View();
        }
    }
}