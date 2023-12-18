﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookBlogApp.EFDbContext;
using BookBlogApp.Models;

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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "bookImage");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + book.BookImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    book.BookImg.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "notValid";
            }

            
            return uniqueFileName;
        }

        [ActionName("Index")]
        public IActionResult BookIndex()
        {
            List<BookDataModel> lst = _context.Books.ToList();
            return View("BookIndex", lst);
        }
        [ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BookCreate(BookDataModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadFile(book);
                    book.ImageUrl = uniqueFileName;
                    _context.Attach(book);
                    _context.Entry(book).State = EntityState.Added;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured: {ErrorMessage}", ex.Message);
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction(nameof(Index));
            //return View(movie);
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

        //Edit
        [ActionName("Edit")]
        public async Task<IActionResult> BookEdit(int id)
        {
            if (!await _context.Books.AsNoTracking().AnyAsync(x => x.Id == id))
            {
                TempData["Message"] = "No data Found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }

            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }
            return View("BookEdit", book);
        }


        //Update
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BookUpdate(int id, BookDataModel reqModel)
        {
            if (!await _context.Books.AsNoTracking().AnyAsync(x => x.Id == id))
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }

            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }

            book.Title = reqModel.Title;
            book.Author = reqModel.Author;
            book.Price = reqModel.Price;
            book.Genre = reqModel.Genre;
            book.PublishedDate = reqModel.PublishedDate;
            book.ImageUrl = reqModel.ImageUrl;

            int result = _context.SaveChanges();
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/book");
        }

        //Delete
        [ActionName("Delete")]
        public async Task<IActionResult> BookDelete(int id)
        {
            if (!await _context.Books.AsNoTracking().AnyAsync(x => x.Id == id))
            {
                TempData["Message"] = "No Data Found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }

            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (book is null)
            {
                TempData["Message"] = "No data found";
                TempData["IsSuccess"] = false;
                return Redirect("/book");
            }

            _context.Remove(book);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/book");
        }
    }
}
