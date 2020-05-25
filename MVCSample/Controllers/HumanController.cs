using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSample.Models.Infestation;
using System.Data;
using System.Linq;

namespace MVCSample.Controllers
{
    public class HumanController : Controller
    {
        InfestationContext db;

        public HumanController(InfestationContext _context)
        {
            db = _context;
        }

        public IActionResult Index(int id)
        {
            ViewData["Human"] = id!=0 ?  db.Humans.Where(x => x.Id == id).ToList() : db.Humans.ToList();
            return View();
        }

        public IActionResult Country(int id)
        {
            //ViewData["COuntryName"] = db.Humans.Include(x => x.Country).Where(x => x.Id == id).SingleOrDefault().Country.Name;
            ViewData["COuntryName"] = db.Humans.Where(x => x.Id == id).SingleOrDefault().Country.Name;
            return View();
        }

        public IActionResult Brazil()
        {
            //ViewData["Brazil"] = db.Humans.Include(x=>x.Country).Where(x=>x.Country.Name == "Brazil").ToList();
            ViewData["Brazil"] = db.Humans.Where(x => x.Country.Name == "Brazil").ToList();
            return View();
        }
    }
}
