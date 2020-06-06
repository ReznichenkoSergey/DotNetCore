using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiExample.Models;

namespace RestApiExample.Controllers
{
    [ApiController]
    [Route("v1/News")]
    public class MockNewsController : ControllerBase
    {
        INewsRepository db;

        public MockNewsController(INewsRepository repository)
        {
            db = repository;
        }

        [HttpGet]
        public IEnumerable<News> GetNew([FromHeader] bool? isFake)
        {
            if(isFake.HasValue)
                return db.GetAllNew().Where(x => x.IsFake == isFake.Value);
            else
                return db.GetAllNew();
        }

        [HttpDelete("{id}")]
        public void DeleteNews(int id)
        {
            var news = db.GetAllNew().FirstOrDefault(x => x.Id == id);
            if (news != null)
            {
                db.DeleteNews(id);
            }
        }

        [HttpPut("{id}")]
        public void UpdateNews(int id, [FromBody] News news)
        {
            if (news.Id == id)
            {
                var temp = db.GetAllNew().FirstOrDefault(x => x.Id == id);
                if (news != null)
                {
                    temp.Title = news.Title;
                    temp.Text = news.Text;
                    temp.AuthorName = news.AuthorName;
                    temp.IsFake = news.IsFake;
                }
            }
        }

        [HttpPatch("{id}")]
        public void UpdateNewsPartial(int id, [FromBody] News news)
        {
            if (news.Id == id)
            {
                var temp = db.GetAllNew().FirstOrDefault(x => x.Id == id);
                if (news != null)
                {
                    if (news.Title != null) temp.Title = news.Title;
                    if (news.Text != null) temp.Text = news.Text;
                    if (news.AuthorName != null) temp.AuthorName = news.AuthorName;
                    if (news.IsFake.HasValue) temp.IsFake = news.IsFake;
                }
            }
        }

        [HttpPost]
        public void CreateNews([FromBody] News news)
        {
            if (db.GetAllNew().Any(x => x.Id == news.Id))
            {
                news.Id = db.GetAllNew().Max(x => x.Id) + 1;
            }
            db.CreateNews(news);
        }
    }
}
