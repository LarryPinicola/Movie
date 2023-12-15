using BookBlogApp.EFDbContext;
using BookBlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBlogApp.Controllers
{
    public class BookController : Controller
    {
        // DependencyInjection
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<BookController> _logger;

        public BookController(AppDbContext context, IWebHostEnvironment webHost, ILogger<BookController> logger)
        {
            _context = context;
            webHostEnvironment = webHost;
            _logger = logger;
        }

        private string UploadFile(BookDataModel book)
        {
            string uniqueFileName = "";
            if (book.BookImg != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "bookImg");
                uniqueFileName = Guid.NewGuid() + "_" + book.BookImg.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    book.BookImg.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [ActionName("Index")]
        public IActionResult BookIndex()
        {
            List<BookDataModel> lst = _context.Books.ToList();
            return View("BookIndex", lst);
        }

        //Create
        [ActionName("Create")]
        public IActionResult BookCreate()
        {
            return View("BookCreate");
        }

        //Save
        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BookSave(BookDataModel reqModel)
        {
            await _context.Books.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/book");
        }
    }
}
