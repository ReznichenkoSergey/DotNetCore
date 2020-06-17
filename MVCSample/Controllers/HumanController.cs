using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSample.Models;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using MVCSample.Models.Repositories;
using MVCSample.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MVCSample.Controllers
{
    public class HumanController : Controller
    {
        readonly IHumanRepository Repository;

        public HumanController(IHumanRepository repository)
        {
            Repository = repository;
        }

        public IActionResult Index(int humanId)
        {
            List<Human> human = null;
            if (humanId != 0)
            {
                var obj = Repository.GetHuman(humanId);
                if (obj != null)
                {
                    ViewData["HumanInfo"] = $"Single Human by the humanid= {humanId}";
                    human = obj != null ? new List<Human>() { obj } : new List<Human>();
                }
                else
                    return new NotFoundObjectResult($"Human was not found!");
            }
            else
            {
                ViewData["HumanInfo"] = "All Humans";
                human = Repository.GetAllHumans().ToList();
            }
            return View(human);
        }

        public IActionResult Authors([FromServices] INewsRepository news, int? humanId)
        {
            var newsList = humanId.HasValue ? news.GetAllNew().Where(x => x.AuthorId == humanId).ToList() : news.GetAllNew().ToList();
            var humanList = humanId.HasValue ? Repository.GetAllHumans().Where(x => x.Id == humanId).ToList() : Repository.GetAllHumans().ToList();

            var temp = humanList
                .GroupJoin(
                newsList,
                x => x.Id,
                y => y.AuthorId,
                (x, y) => new HumanAuthorsViewModel {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NewsCounter = y.Count() 
                })
                .ToList();

            return View(temp);
        }

        public IActionResult Country([FromQuery] string name)
        {
            ViewData["CountryName"] = name;
            var humans = Repository.GetAllHumans()
                .Where(x => x.Country.Name == name)
                .ToList();
            return View(humans);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Human human)
        {
            if(ModelState.IsValid)
                Repository.CreateHuman(human);
            return View();
        }

    }
}
