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
    public class TheatorsController : Controller
    {
        private readonly DatabaseContext _context;

        public TheatorsController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: Theators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Theator.ToListAsync());
        }

        // GET: Theators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theator = await _context.Theator
                .SingleOrDefaultAsync(m => m.Id == id);
            if (theator == null)
            {
                return NotFound();
            }

            return View(theator);
        }

        // GET: Theators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Theators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,City")] Theator theator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(theator);
        }

        // GET: Theators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theator = await _context.Theator.SingleOrDefaultAsync(m => m.Id == id);
            if (theator == null)
            {
                return NotFound();
            }
            return View(theator);
        }

        // POST: Theators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City")] Theator theator)
        {
            if (id != theator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheatorExists(theator.Id))
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
            return View(theator);
        }

        // GET: Theators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theator = await _context.Theator
                .SingleOrDefaultAsync(m => m.Id == id);
            if (theator == null)
            {
                return NotFound();
            }

            return View(theator);
        }

        // POST: Theators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theator = await _context.Theator.SingleOrDefaultAsync(m => m.Id == id);
            _context.Theator.Remove(theator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TheatorExists(int id)
        {
            return _context.Theator.Any(e => e.Id == id);
        }

        // GET: Theators/Map
        public IActionResult Map()
        {
            return View();
        }
    }
}
