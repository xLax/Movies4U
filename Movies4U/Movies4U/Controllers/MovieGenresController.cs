using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies4U.Models;

namespace Movies4U.Controllers
{
    public class MovieGenresController : Controller
    {
        private readonly DatabaseContext _context;

        public MovieGenresController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: MovieGenres
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.MovieGenres.Include(m => m.Genre).Include(m => m.Movie);
            return View(await databaseContext.ToListAsync());
        }

        // GET: MovieGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenres = await _context.MovieGenres
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieGenres == null)
            {
                return NotFound();
            }

            return View(movieGenres);
        }

        // GET: MovieGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            return View();
        }

        // POST: MovieGenres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,GenreId")] MovieGenres movieGenres)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieGenres);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", movieGenres.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieGenres.MovieId);
            return View(movieGenres);
        }

        // GET: MovieGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenres = await _context.MovieGenres.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieGenres == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", movieGenres.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieGenres.MovieId);
            return View(movieGenres);
        }

        // POST: MovieGenres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,GenreId")] MovieGenres movieGenres)
        {
            if (id != movieGenres.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieGenres);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenresExists(movieGenres.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", movieGenres.GenreId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieGenres.MovieId);
            return View(movieGenres);
        }

        // GET: MovieGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenres = await _context.MovieGenres
                .Include(m => m.Genre)
                .Include(m => m.Movie)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieGenres == null)
            {
                return NotFound();
            }

            return View(movieGenres);
        }

        // POST: MovieGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieGenres = await _context.MovieGenres.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.MovieGenres.Remove(movieGenres);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieGenresExists(int id)
        {
            return _context.MovieGenres.Any(e => e.MovieId == id);
        }
    }
}
