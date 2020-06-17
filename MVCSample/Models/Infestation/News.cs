using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSample.Models.Infestation
{
    public class News
    {
        public int Id { get; set; }
        
        [Required, Range(5, 300)]
        public string Title { get; set; }
        
        public string Text { get; set; }
        
        [Required]
        public bool IsFake { get; set; }

        [Required, Range(0, 1000000)]        
        public int AuthorId { get; set; }
        public virtual Human Author { get; set; }
    }
}
