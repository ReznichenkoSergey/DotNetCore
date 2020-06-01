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
        InfestationContext db;

        public SqlHumanRepository(InfestationContext context)
        {
            db = context;
        }

        public void CreateHuman(Human obj)
        {
            db.Humans.Add(obj);
            db.SaveChanges();
        }

        public IQueryable<Human> GetAllHumans() => db.Humans.AsQueryable();

        public Human GetHuman(int id) => db.Humans.Where(x=>x.Id == id).FirstOrDefault();

        public void DeleteHuman(int id)
        {
            db.Humans.Remove(db.Humans.Find(id));
            db.SaveChanges();
        }

        public void ModifyHuman(Human obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
