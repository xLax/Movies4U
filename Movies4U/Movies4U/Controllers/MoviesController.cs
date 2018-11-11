using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies4U.Models;
using Newtonsoft.Json;

namespace Movies4U.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DatabaseContext _context;

        public MoviesController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies
        public async Task<IActionResult> AdminSearch()
        {
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Year,NumOfMinutes,Summary,MinimumAge,TrailerURL")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Year,NumOfMinutes,Summary,MinimumAge,TrailerURL")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .SingleOrDefaultAsync(m => m.Id == id);
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
            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.Id == id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }

        // GET: Courses
        public JsonResult Search(string MovieName)
        {
            List<Movie> movies; 
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            if (MovieName == null)
            {
                movies = ( from m in _context.Movie
                           select m ).ToList();
            }
            else
            {
                movies = ( from m in _context.Movie
                           where m.Name.Contains(MovieName)
                           select m ).ToList();
            }

            return Json(movies, serializerSettings);
        }

        public JsonResult GetGenres()
        {
            List<Genre> genres; 
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            genres = (from g in _context.Genre
                      select g).ToList();

            return Json(genres, serializerSettings);
        }

        public JsonResult GetLanguages()
        {
            List<Language> languages;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            languages = (from l in _context.Language
                         select l).ToList();

            return Json(languages, serializerSettings);
        }

        public JsonResult AdminSearchAction(string MovieName, string SearchGenre, string SearchLanguage)
        {
            List<Movie> movies;
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            int genreID = int.Parse(SearchGenre);
            int languageID = int.Parse(SearchLanguage);

            serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            if (MovieName == null)
            {
                if(genreID == 0)
                {
                    if(languageID == 0)
                    {
                        // name genre and language are empty
                        movies = (from m in _context.Movie
                                  select m).ToList();
                    }
                    else
                    {
                        // name and genre empty
                        // language is full
                        movies = (from m in _context.Movie
                                  join ml in _context.MovieLanguages on 
                                  new
                                  {
                                      movieId = m.Id,
                                      movieLanId = languageID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = ml.MovieId,
                                      movieLanId = ml.LanguageId
                                  }
                                  select m).ToList();
                    }
                }
                else
                {
                    if (languageID == 0)
                    {
                        // name and language empty
                        // genre is full
                        movies = (from m in _context.Movie
                                  join mg in _context.MovieGenres on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieGenId = genreID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = mg.MovieId,
                                      movieGenId = mg.GenreId
                                  }
                                  select m).ToList();
                    }
                    else
                    {
                        // name is empty
                        // language and genre are full
                        movies = (from m in _context.Movie
                                  join ml in _context.MovieLanguages on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieLanId = languageID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = ml.MovieId,
                                      movieLanId = ml.LanguageId
                                  }
                                  join mg in _context.MovieGenres on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieGenId = genreID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = mg.MovieId,
                                      movieGenId = mg.GenreId
                                  }
                                  select m).ToList();
                    }
                }
                
            }
            else
            {
                if (genreID == 0)
                {
                    if (languageID == 0)
                    {
                        // language and genre empty
                        // name is full
                        movies = (from m in _context.Movie
                                  where m.Name.Contains(MovieName)
                                  select m).ToList();
                    }
                    else
                    {
                        // genre empty
                        // name and language are full
                        movies = (from m in _context.Movie
                                  join ml in _context.MovieLanguages on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieLanId = languageID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = ml.MovieId,
                                      movieLanId = ml.LanguageId
                                  }
                                  where m.Name.Contains(MovieName)
                                  select m).ToList();
                    }
                }
                else
                {
                    if (languageID == 0)
                    {
                        // name and genre are full
                        // language is empty
                        movies = (from m in _context.Movie
                                  join mg in _context.MovieGenres on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieGenId = genreID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = mg.MovieId,
                                      movieGenId = mg.GenreId
                                  }
                                  where m.Name.Contains(MovieName)
                                  select m).ToList();
                    }
                    else
                    {
                        // name language and genre are full
                        movies = (from m in _context.Movie
                                  join ml in _context.MovieLanguages on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieLanId = languageID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = ml.MovieId,
                                      movieLanId = ml.LanguageId
                                  }
                                  join mg in _context.MovieGenres on
                                  new
                                  {
                                      movieId = m.Id,
                                      movieGenId = genreID
                                  }
                                  equals
                                  new
                                  {
                                      movieId = mg.MovieId,
                                      movieGenId = mg.GenreId
                                  }
                                  where m.Name.Contains(MovieName)
                                  select m).ToList();
                    }
                }
            }

            return Json(movies, serializerSettings);
        }
    }
}
