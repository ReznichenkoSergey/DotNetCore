using MVCSample.Models.Infestation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.ViewModels
{
    public class HumanAuthorsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NewsCounter { get; set; }
    }
}
