using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InformationSystemDomain.Model;
using InformationSystemInfrastructure;
using InformationSystemInfrastructure.Services;

namespace InformationSystemInfrastructure.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ProjectCsContext _context;

        public SubjectsController(ProjectCsContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subjects.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Description,Name")] Subject subject)
        {
            var existingPublication = await _context.Subjects
                 .Where(u => EF.Functions.Like(u.Name, subject.Name))
                 .FirstOrDefaultAsync();

            if (existingPublication != null)
            {
                ModelState.AddModelError("Name", "Subject with the same name already exists.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Description,Name")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            var existingPublication = await _context.Subjects
                 .Where(u => EF.Functions.Like(u.Name, subject.Name))
                 .FirstOrDefaultAsync();

            if (existingPublication != null)
            {
                ModelState.AddModelError("Name", "Subject with the same name already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Cascade test
            var relatedarticles = await _context.Subjects
                .Include(s => s.Articles)
                    .ThenInclude(a => a.Comments)
                .Include(s => s.Articles)
                    .ThenInclude(a => a.AuthorsPerArticles)
                .FirstOrDefaultAsync(s => s.SubjectId == id);
            if (relatedarticles != null)
            {
                foreach (var article in relatedarticles.Articles)
                {
                    _context.Comments.RemoveRange(article.Comments);
                    _context.AuthorsPerArticles.RemoveRange(article.AuthorsPerArticles);
                }
                _context.Articles.RemoveRange(relatedarticles.Articles);
            }
            //
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel, CancellationToken cancellationToken = default)
        {
            var importService = new SubjectDataPortServiceFactory(_context).GetImportService(fileExcel.ContentType);
            using var stream = fileExcel.OpenReadStream();
            await importService.ImportFromStreamAsync(stream, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Export([FromQuery] string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        CancellationToken cancellationToken = default)
        {
            var exportService = new SubjectDataPortServiceFactory(_context).GetExportService(contentType);

            var memoryStream = new MemoryStream();

            await exportService.WriteToAsync(memoryStream, cancellationToken);

            await memoryStream.FlushAsync(cancellationToken);
            memoryStream.Position = 0;


            return new FileStreamResult(memoryStream, contentType)
            {
                FileDownloadName = $"categiries_{DateTime.UtcNow.ToShortDateString()}.xlsx"
            };
        }
        }
    }
