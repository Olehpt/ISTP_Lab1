﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InformationSystemDomain.Model;
using InformationSystemInfrastructure;

namespace InformationSystemInfrastructure.Controllers
{
    public class UsersController : Controller
    {
        private readonly ProjectCsContext _context;

        public UsersController(ProjectCsContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,Email,Password,Info,Contacts,Avatar,SignUpDate")] User user)
        {
            var existingUser = await _context.Users
                 .Where(u => EF.Functions.Like(u.Email, user.Email))
                 .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "User with the same email already exists.");
            }

            if (user.SignUpDate > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(nameof(user.SignUpDate), "Not actual date.");
                return View(user);
            }
            var mindate = new DateOnly(2000, 1, 1);
            if (user.SignUpDate < mindate)
            {
                ModelState.AddModelError(nameof(user.SignUpDate), "Not actual date.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Email,Password,Info,Contacts,Avatar,SignUpDate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            var existingUser = await _context.Users
                 .Where(u => EF.Functions.Like(u.Email, user.Email))
                 .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "User with the same email already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Cascade test
            var relatedauthorships = await _context.Users
                .Include(s => s.AuthorsPerArticles)
                .FirstOrDefaultAsync(s => s.UserId == id);
            if (relatedauthorships != null) { _context.Users.RemoveRange(relatedauthorships); }
            var relatedcoms = await _context.Comments
                .Where(s => s.AuthorId == id)
                .ToListAsync();
            if (relatedauthorships != null) { _context.Comments.RemoveRange(relatedcoms); }
            //
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
