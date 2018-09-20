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
    public class MovieLanguagesController : Controller
    {
        private readonly DatabaseContext _context;

        public MovieLanguagesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: MovieLanguages
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.MovieLanguages.Include(m => m.Language).Include(m => m.Movie);
            return View(await databaseContext.ToListAsync());
        }

        // GET: MovieLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieLanguages = await _context.MovieLanguages
                .Include(m => m.Language)
                .Include(m => m.Movie)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieLanguages == null)
            {
                return NotFound();
            }

            return View(movieLanguages);
        }

        // GET: MovieLanguages/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Name");
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Name");
            return View();
        }

        // POST: MovieLanguages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,LanguageId")] MovieLanguages movieLanguages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieLanguages);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movieLanguages.LanguageId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieLanguages.MovieId);
            return View(movieLanguages);
        }

        // GET: MovieLanguages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieLanguages = await _context.MovieLanguages.SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieLanguages == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movieLanguages.LanguageId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieLanguages.MovieId);
            return View(movieLanguages);
        }

        // POST: MovieLanguages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,LanguageId")] MovieLanguages movieLanguages)
        {
            if (id != movieLanguages.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieLanguages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieLanguagesExists(movieLanguages.MovieId))
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
            ViewData["LanguageId"] = new SelectList(_context.Language, "Id", "Id", movieLanguages.LanguageId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", movieLanguages.MovieId);
            return View(movieLanguages);
        }

        // GET: MovieLanguages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieLanguages = await _context.MovieLanguages
                .Include(m => m.Language)
                .Include(m => m.Movie)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (movieLanguages == null)
            {
                return NotFound();
            }

            return View(movieLanguages);
        }

        // POST: MovieLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieLanguages = await _context.MovieLanguages.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.MovieLanguages.Remove(movieLanguages);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieLanguagesExists(int id)
        {
            return _context.MovieLanguages.Any(e => e.MovieId == id);
        }
    }
}
