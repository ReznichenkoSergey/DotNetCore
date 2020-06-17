using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Models.Infestation
{
    public class Human
    {
        
        public int Id { get; set; }
        [Required,
            RegularExpression("^[a-zA-Z]*&", ErrorMessage ="The field contains wrong symbols!"),
            Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Range(1,100, ErrorMessage ="Age is out of range")]
        public int Age { get; set; }
        [Required]
        public bool IsSick { get; set; }
        [Required]
        public string Gender { get; set; }
        [Range(1, 250, ErrorMessage = "Country index is out of range")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
