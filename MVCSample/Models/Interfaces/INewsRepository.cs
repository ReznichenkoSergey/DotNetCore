using MVCSample.Models.Infestation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Interfaces
{
    public interface INewsRepository
    {
        IQueryable<News> GetAllNew();
        News GetNews(int id);
        void CreateNews(News obj);
        void ModifyNews(News obj);
        void DeleteNews(int id);
    }
}
