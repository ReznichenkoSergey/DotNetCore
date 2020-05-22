using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Controllers
{
    public class HumanController
    {
        public IActionResult Index()
        {
            return new BadRequestResult();
        }
    }
}
