using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
    //[Authorize]
    public class HumanController : Controller
    {
        readonly IHumanRepository Repository;

        public HumanController(IHumanRepository repository)
        {
            Repository = repository;
        }

        [AllowAnonymous]
        public IActionResult Index(int[] humanId)
        {
            List<Human> human = null;
            if (humanId.Length > 0)
            {
                ViewData["HumanInfo"] = humanId.Length == 1 ? $"Single Human by the humanid= {humanId[0]}" : "Humans From Array";
                human = Repository.GetAllHumans().Where(x => humanId.Contains(x.Id)).ToList();
            }
            else
            {
                ViewData["HumanInfo"] = "All Humans";
                human = Repository.GetAllHumans().ToList();
            }
            return View(human);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        public IActionResult Country([FromQuery] string name)
        {
            ViewData["CountryName"] = name;
            var humans = Repository.GetAllHumans()
                .Where(x => x.Country.Name == name)
                .ToList();
            if (humans.Count > 0)
            {
                string m = string.Join("&", humans.Select(x => $"humanId={x.Id}"));
                return Redirect($"~/Human/Index?{m}");
            }
            else
                return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Human human)
        {
            if(ModelState.IsValid)
                Repository.CreateHuman(human);
            return View();
        }

    }
}
