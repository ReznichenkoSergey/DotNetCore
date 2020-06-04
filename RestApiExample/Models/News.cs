using System.Collections.Generic;

namespace RestApiExample.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool? IsFake { get; set; }
        public string AuthorName { get; set; }
    }
}
