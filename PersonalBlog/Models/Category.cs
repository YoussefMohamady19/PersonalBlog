using System.Collections.Generic;
namespace PersonalBlog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relationship with BlogPost
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
