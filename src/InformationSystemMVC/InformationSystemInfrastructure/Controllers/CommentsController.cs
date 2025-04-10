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
    public class CommentsController : Controller
    {
        private readonly ProjectCsContext _context;

        public CommentsController(ProjectCsContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var projectCsContext = _context.Comments.Include(c => c.Article).Include(c => c.Author);
            return View(await projectCsContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Article)
                .Include(c => c.Author)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "Name");
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,Content,PublicationDate,ArticleId,AuthorId")] Comment comment)
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "Name", comment.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "Email", comment.AuthorId);

            if (comment.PublicationDate > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(nameof(comment.PublicationDate), "Not actual date.");
                return View(comment);
            }
            var selectedArticleId = ViewData["ArticleId"] as SelectList;
            int? articleId = selectedArticleId?.SelectedValue as int?;
            var currentArticle = _context.Articles.FirstOrDefault(a => a.ArticleId == articleId);
            if (currentArticle != null)
            {
                var mindate = currentArticle.PublicationDate;
                if (comment.PublicationDate < mindate)
                {
                    ModelState.AddModelError(nameof(comment.PublicationDate), "Not actual date.");
                    return View(comment);
                }
            }
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "Name", comment.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "Email", comment.AuthorId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComId,Content,PublicationDate,ArticleId,AuthorId")] Comment comment)
        {
            if (id != comment.ComId)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "ArticleId", "Name", comment.ArticleId);
            ViewData["AuthorId"] = new SelectList(_context.Users, "UserId", "Email", comment.AuthorId);
            try
            {
                _context.Update(comment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(comment.ComId))
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

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Article)
                .Include(c => c.Author)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.ComId == id);
        }
    }
}
