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
    public class AuthorsPerArticlesController : Controller
    {
        private readonly ProjectCsContext _context;

        public AuthorsPerArticlesController(ProjectCsContext context)
        {
            _context = context;
        }

        // GET: AuthorsPerArticles
        public async Task<IActionResult> Index()
        {
            var projectCsContext = _context.AuthorsPerArticles.Include(a => a.ArticleNavigation).Include(a => a.AuthorsNavigation);
            return View(await projectCsContext.ToListAsync());
        }

        // GET: AuthorsPerArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPerArticle = await _context.AuthorsPerArticles
                .Include(a => a.ArticleNavigation)
                .Include(a => a.AuthorsNavigation)
                .FirstOrDefaultAsync(m => m.LinkId == id);
            if (authorsPerArticle == null)
            {
                return NotFound();
            }

            return View(authorsPerArticle);
        }

        // GET: AuthorsPerArticles/Create
        public IActionResult Create()
        {
            ViewData["Article"] = new SelectList(_context.Articles, "ArticleId", "Name");
            ViewData["Authors"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: AuthorsPerArticles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Authors,Article")] AuthorsPerArticle authorsPerArticle)
        {
            int maxId = _context.AuthorsPerArticles.Any()
             ? _context.AuthorsPerArticles.Max(x => x.LinkId) + 1
             : 1;
            authorsPerArticle.LinkId = maxId;
            ViewData["Article"] = new SelectList(_context.Articles, "ArticleId", "Name", authorsPerArticle.Article);
            ViewData["Authors"] = new SelectList(_context.Users, "UserId", "Email", authorsPerArticle.Authors);
            _context.Add(authorsPerArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: AuthorsPerArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPerArticle = await _context.AuthorsPerArticles.FindAsync(id);
            if (authorsPerArticle == null)
            {
                return NotFound();
            }
            ViewData["Article"] = new SelectList(_context.Articles, "ArticleId", "Name", authorsPerArticle.Article);
            ViewData["Authors"] = new SelectList(_context.Users, "UserId", "Email", authorsPerArticle.Authors);
            return View(authorsPerArticle);
        }

        // POST: AuthorsPerArticles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LinkId,Authors,Article")] AuthorsPerArticle authorsPerArticle)
        {
            if (id != authorsPerArticle.LinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorsPerArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsPerArticleExists(authorsPerArticle.LinkId))
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
            ViewData["Article"] = new SelectList(_context.Articles, "ArticleId", "Name", authorsPerArticle.Article);
            ViewData["Authors"] = new SelectList(_context.Users, "UserId", "Email", authorsPerArticle.Authors);
            return View(authorsPerArticle);
        }

        // GET: AuthorsPerArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPerArticle = await _context.AuthorsPerArticles
                .Include(a => a.ArticleNavigation)
                .Include(a => a.AuthorsNavigation)
                .FirstOrDefaultAsync(m => m.LinkId == id);
            if (authorsPerArticle == null)
            {
                return NotFound();
            }

            return View(authorsPerArticle);
        }

        // POST: AuthorsPerArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorsPerArticle = await _context.AuthorsPerArticles.FindAsync(id);
            if (authorsPerArticle != null)
            {
                _context.AuthorsPerArticles.Remove(authorsPerArticle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsPerArticleExists(int id)
        {
            return _context.AuthorsPerArticles.Any(e => e.LinkId == id);
        }
    }
}
