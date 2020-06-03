using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiExample.Models;

namespace RestApiExample.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        INewsRepository db;

        public NewsController(INewsRepository repository)
        {
            db = repository;
        }

        [HttpGet("News")]
        public IEnumerable<News> GetNews()
        {
            return db.GetAllNew();
        }

        [HttpPost("News")]
        public void CreateNews([FromBody] News news)
        {
            db.CreateNews(news);
        }
    }
}
