using BookBlogApp.EFDbContext;
using BookBlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBlogApp.Controllers
{
    public class BookController : Controller
    {
        // DependencyInjection
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult BookIndex()
        {
            List<BookDataModel> lst = _context.Blogs.ToList();
            return View("BookIndex",lst);
        }
    }
}
