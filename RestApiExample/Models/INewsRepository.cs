using System.Collections.Generic;
using System.Linq;

namespace RestApiExample.Models
{
    public interface INewsRepository
    {
        List<News> GetAllNew();
        void CreateNews(News news);
        void DeleteNews(int id);
    }
}
