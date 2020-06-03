using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Infestation
{
    public class Human
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsSick { get; set; }
        public string Gender { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
