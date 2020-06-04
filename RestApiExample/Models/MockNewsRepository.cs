using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiExample.Models
{
    public class MockNewsRepository : INewsRepository
    {
        List<News> News = new List<News>
        {
            new News { Id = 0, Title = "Humanity finally colonized the Mercury!!", Text = "", AuthorName = "Jeremy Clarkson", IsFake = true},
            new News { Id = 1, Title = "Increase your lifespan by 10 years, every morning you need...", Text = "", AuthorName = "Svetlana Sokolova", IsFake = true},
            new News { Id = 2, Title = "Scientists estimed the time of the vaccine invension: it is a summer of 2021", Text = "", AuthorName = "John Jones", IsFake = false},
            new News { Id = 3, Title = "Ukraine reduces the cost of its obligations!", Text = "", AuthorName = "Cerol Denvers", IsFake = false},
            new News { Id = 4, Title = "A species were discovered in Africa: it is blue legless cat", Text = "", AuthorName = "Jimmy Felon", IsFake = true}
        };

        public List<News> GetAllNew() => News;
        
        public News GetNews(int id) => News.Where(x=>x.Id == id).FirstOrDefault();

        public void CreateNews(News news)
        {
            if(News.Any(x=>x.Id == news.Id))
            {
                news.Id = News.Max(x => x.Id) + 1;
            }
            News.Add(news);
        }

        public void DeleteNews(int id)
        {
            var news = News.FirstOrDefault(x => x.Id == id);
            if (news != null)
            {
                News.Remove(news);
            }
        }

        public void UpdateNews(int id, News news)
        {
            if (news.Id == id)
            {
                var temp = News.FirstOrDefault(x => x.Id == id);
                if (news != null)
                {
                    if (news.Title != null)
                        temp.Title = news.Title;
                    if (news.Text != null)
                        temp.Text = news.Text;
                    if (news.AuthorName != null)
                        temp.AuthorName = news.AuthorName;
                    if (news.IsFake.HasValue)
                        temp.IsFake = news.IsFake;

                }
            }
        }
    }
}
