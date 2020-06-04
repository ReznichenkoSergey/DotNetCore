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
        public List<News> GetAllNew() => db.GetAllNew();

        [HttpDelete("{id}")]
        public void DeleteNews(int id)
        {
            db.DeleteNews(id);
        }

        [HttpPut("{id}")]
        public void UpdateNews(int id, [FromBody] News news)
        {
            db.UpdateNews(id, news);
        }

        [HttpPatch("{id}")]
        public void UpdateNewsPartial(int id, [FromBody] News news)
        {
            db.UpdateNews(id, news);
        }

        [HttpPost]
        public void CreateNews([FromHeader] bool isFake, [FromBody] News news)
        {
            news.IsFake = isFake;
            db.CreateNews(news);
        }
    }
}
