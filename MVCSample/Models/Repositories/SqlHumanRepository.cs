using Microsoft.EntityFrameworkCore;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MVCSample.Models.Repositories
{
    public class SqlHumanRepository : IHumanRepository
    {
        DbContext db;
        DbSet<Human> human;

        public SqlHumanRepository(InfestationContext context)
        {
            db = context;
            human = db.Set<Human>();
        }

        public void CreateHuman(Human obj)
        {
            human.Add(obj);
            db.SaveChanges();
        }

        public IQueryable<Human> GetAllHumans() => human.AsQueryable();

        public Human GetHuman(int id) => human.Where(x=>x.Id == id).FirstOrDefault();

        public void DeleteHuman(int id)
        {
            human.Remove(human.Find(id));
            db.SaveChanges();
        }

        public void ModifyHuman(Human obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
