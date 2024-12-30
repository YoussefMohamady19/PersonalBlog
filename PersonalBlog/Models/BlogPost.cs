using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace PersonalBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        // Category relationship
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Comments
        public ICollection<Comment> Comments { get; set; }
    }
}
