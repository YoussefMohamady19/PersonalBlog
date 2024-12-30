using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;
using System.Linq;
namespace PersonalBlog.Controllers
{
    public class BlogController:Controller
    {
        private readonly BlogDbContext _context;

        public BlogController(BlogDbContext context)
        {
            _context = context;
        }

        // Index Action - Display all blog posts
        public IActionResult Index()
        {
            var posts = _context.BlogPosts.Include(b => b.Category).ToList();
            return View(posts);
        }

        // Show a single blog post
        public IActionResult Details(int id)
        {
            var post = _context.BlogPosts
                .Include(b => b.Category)
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // Create New Blog Post - Get
        public IActionResult Create()
        {
            var model = new BlogPost();

            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        // Create New Blog Post - Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost post)
        {
            if (post.Title.Length>0&&post.Content.Length>0)
            {
                post.PublishedDate = DateTime.Now;
                _context.BlogPosts.Add(post);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(int id, string author, string content)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(content))
            {
                return RedirectToAction(nameof(Details), new { id });
            }

            var comment = new Comment
            {
                BlogPostId = id,
                Author = author,
                Content = content,
                PostedDate = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
