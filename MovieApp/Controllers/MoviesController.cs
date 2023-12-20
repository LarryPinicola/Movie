using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieAppContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<MoviesController> _logger;


        public MoviesController(MovieAppContext context, IWebHostEnvironment webHost, ILogger<MoviesController> logger)
        {
            _context = context;
            webHostEnvironment = webHost;
            _logger = logger;
        }

        private string UploadFile(Movie movie)
        {
            string uniqueFileName = "";
            if (movie.MovieImg != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "movieImg");
                if (!string.IsNullOrEmpty(movie.ImageUrl))
                {
                    string existingFilePath = Path.Combine(webHostEnvironment.WebRootPath, movie.ImageUrl);
                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + movie.MovieImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    movie.MovieImg.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "notValid";
            }
            return uniqueFileName;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return _context.Movie != null ?
                        View(await _context.Movie.ToListAsync()) :
                        Problem("Entity set 'MovieAppContext.Movie'  is null.");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            try
            {
                string uniqueFileName = UploadFile(movie);
                movie.ImageUrl = uniqueFileName;
                _context.Attach(movie);
                _context.Entry(movie).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured: {ErrorMessage}", ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            Movie movie = new Movie();
            return View(movie);
        }

        // GET: Movies/Edit/5
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovie = await _context.Movie.FindAsync(id);
                    if (existingMovie == null)
                    {
                        return NotFound();
                    }
                    existingMovie.Title = movie.Title;
                    existingMovie.ReleaseDate = movie.ReleaseDate;
                    existingMovie.Genre = movie.Genre;
                    existingMovie.Price = movie.Price;
                    existingMovie.ImageUrl = movie.ImageUrl;
                    if (movie.MovieImg != null)
                    {
                        movie.ImageUrl = existingMovie.ImageUrl;
                        string newImagePath = UploadFile(movie);
                        existingMovie.ImageUrl = newImagePath;
                    }
                    _context.Update(existingMovie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "An error occured: {ErrorMessage}", ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MovieAppContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
