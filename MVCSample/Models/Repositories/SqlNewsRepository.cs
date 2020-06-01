using Microsoft.EntityFrameworkCore;
using MVCSample.Models.Infestation;
using MVCSample.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Repositories
{
    public class SqlNewsRepository : INewsRepository
    {
        InfestationContext Context;
        public SqlNewsRepository(InfestationContext context)
        {
            Context = context;
        }

        public void CreateNews(News obj)
        {
            Context.News.Add(obj);
            Context.SaveChanges();
        }

        public void DeleteNews(int id)
        {
            Context.News.Remove(Context.News.Where(x=>x.Id==id).FirstOrDefault());
            Context.SaveChanges();
        }

        public IQueryable<News> GetAllNew() => Context.News.AsQueryable();
        
        public News GetNews(int id) => Context.News.Where(x=>x.Id == id).FirstOrDefault();

        public void ModifyNews(News obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
