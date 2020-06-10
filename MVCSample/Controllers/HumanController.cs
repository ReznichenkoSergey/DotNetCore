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

        public IActionResult Index([FromQuery] int humanId)
        {
            //ViewData["Humans"] = humanId != 0 ?  db.Humans.Where(x => x.Id == humanId).ToList() : db.Humans.ToList();
            //ViewData["Humans"] = humanId != 0 ? new List<Human>() { Repository.GetHuman(humanId) } : Repository.GetAllHumans().ToList();
            if (humanId != 0)
            {
                var obj = Repository.GetHuman(humanId);
                if (obj != null)
                {
                    ViewData["HumanInfo"] = $"Single Human by the humanid={humanId}";
                    //ViewData["Humans"] = obj != null ? new List<Human>() { obj } : new List<Human>();
                    ViewBag.Humans = obj != null ? new List<Human>() { obj } : new List<Human>();
                }
                else
                    return new NotFoundObjectResult($"Human was not found!");
            }
            else
            {
                ViewData["HumanInfo"] = "All Humans";
                //ViewData["Humans"] = Repository.GetAllHumans().ToList();
                ViewBag.Humans = Repository.GetAllHumans().ToList();
            }
            return View();
        }

        public IActionResult Authors([FromServices] INewsRepository news)
        {
            var newsList = news.GetAllNew().ToList();
            var humanList = Repository.GetAllHumans().ToList();

            var temp = humanList
                .GroupJoin(
                newsList,
                x => x.Id,
                y => y.AuthorId,
                (x, y) => new HumanAuthorsViewModel {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NewsCounter = y.Count() 
                })
                .ToList();

            return View(temp);
        }

        public IActionResult Country([FromQuery] string name)
        {
            /*ViewData["Humans"] = db.Humans
                .Include(x => x.Country)
                .Where(x => x.Country.Name == name)
                .ToList();*/
            ViewData["CountryName"] = name;
            ViewData["Humans"] = Repository.GetAllHumans()
                .Include(x => x.Country)
                .Where(x => x.Country.Name == name)
                .ToList();
            return View();
        }

        //[HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Create(string FirstName, string LastName, int Age, bool IsSick, string Gender, int CountryId)
        public IActionResult Create(Human human)
        {
            /*var human = new Human();

            human.FirstName = FirstName;
            human.LastName = LastName;
            human.Age = Age;
            human.IsSick = IsSick;
            human.Gender = Gender;
            human.CountryId = CountryId;*/

            Repository.CreateHuman(human);

            return View();
        }

    }
}
