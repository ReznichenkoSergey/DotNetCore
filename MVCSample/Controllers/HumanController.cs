using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using MVCSample.Models.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MVCSample.Controllers
{
    public class HumanController : Controller
    {
        IHumanRepository Repository;

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
                    ViewData["Humans"] = obj != null ? new List<Human>() { obj } : new List<Human>();
                }
                else
                    return new NotFoundObjectResult($"Human was not found!");
            }
            else
            {
                ViewData["HumanInfo"] = $"All Humans";
                ViewData["Humans"] = Repository.GetAllHumans().ToList();
            }
            return View();
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
    }
}
