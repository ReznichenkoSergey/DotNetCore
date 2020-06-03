using System.Collections.Generic;

namespace MVCSample.Models.Infestation
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsFake { get; set; }
        public int AuthorId { get; set; }
        public virtual Human Author { get; set; }
    }
}
