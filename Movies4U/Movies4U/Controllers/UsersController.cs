using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Movies4U.Models;

namespace Movies4U.Controllers
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public IActionResult Home()
        {
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Username == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,Birthdate")] Users users)
        {
            if (ModelState.IsValid)
            {
                Users tmp_user = (Users)_context.Users.SingleOrDefault(currUser => users.Username == currUser.Username);
                if (tmp_user == null)
                {
                    _context.Add(users);
                    ModelState.AddModelError("Register", "");
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("Register", "User name already exist");
                }
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Home([Bind("Username,Password")] Users users)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(users);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Username == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Email,Birthdate")] Users users)
        {
            if (id != users.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Username))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Username == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.Username == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Username == id);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] Users user)
        {
            if (_context.Users.Any(u => u.Username == user.Username && u.Password == user.Password))
                {
                if (ModelState.IsValid)
                {
                    Users tmp_user = (Users)_context.Users.Single(currUser => user.Password == currUser.Password && user.Username == currUser.Username);
                    if (tmp_user != null)
                    {
                        HttpContext.Session.SetString("userName", tmp_user.Username);
                        var today = DateTime.Today;
                        var age = today.Year - tmp_user.Birthdate.Year;
                        if (tmp_user.Birthdate > today.AddYears(-age)) age--;
                        if (age >= 16)
                        {
                            HttpContext.Session.SetString("16+", "true");
                        }
                        else
                        {
                            HttpContext.Session.SetString("16+", "false");
                        }

                        if (tmp_user.Username == "chen" && tmp_user.Password == "1234")
                        {
                            HttpContext.Session.SetString("isAdmin", "true");
                        }
                        ViewBag.Error = null;
                        return RedirectToAction("Index", "Movies");
                    }
                }
            }
            else
            {
                ViewBag.Error = "User name or password are incorrecrt";
            }
            return View();
        }

        // GET: Users/Logout
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("userName") != null)
            {
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Admin
        public IActionResult Admin()
        {
            return View();
        }
    }
}
