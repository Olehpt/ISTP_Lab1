using System;
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
    public class ArticlesController : Controller
    {
        private readonly ProjectCsContext _context;

        public ArticlesController(ProjectCsContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var projectCsContext = _context.Articles.Include(a => a.Subject).Include(a => a.Type);
            return View(await projectCsContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Subject)
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            ViewData["TypeId"] = new SelectList(_context.PublicationTypes, "TypeId", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Name,Topic,Content,PublicationDate,Media,SubjectId,TypeId")] Article article)
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", article.SubjectId);
            ViewData["TypeId"] = new SelectList(_context.PublicationTypes, "TypeId", "Name", article.TypeId);

            if (article.PublicationDate > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(nameof(article.PublicationDate), "Well well well");
                return View(article);
            }
            var mindate = new DateOnly(2000, 1, 1);
            if (article.PublicationDate < mindate)
            {
                ModelState.AddModelError(nameof(article.PublicationDate), "Well well well");
                return View(article);
            }

            _context.Add(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", article.SubjectId);
            ViewData["TypeId"] = new SelectList(_context.PublicationTypes, "TypeId", "Name", article.TypeId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Name,Topic,Content,PublicationDate,Media,SubjectId,TypeId")] Article article)
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", article.SubjectId);
            ViewData["TypeId"] = new SelectList(_context.PublicationTypes, "TypeId", "Name", article.TypeId);

            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (article.PublicationDate > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(nameof(article.PublicationDate), "Well well well");
                return View(article);
            }
            var mindate = new DateOnly(2000, 1, 1);
            if (article.PublicationDate < mindate)
            {
                ModelState.AddModelError(nameof(article.PublicationDate), "Well well well");
                return View(article);
            }

            
            try
            {
                _context.Update(article);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(article.ArticleId))
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

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Subject)
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
