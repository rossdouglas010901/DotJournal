using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotJournal.Data;
using DotJournal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DotJournal.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        private readonly JournalDbContext _context;

        public PagesController(JournalDbContext context)
        {
            _context = context;
        }

        // GET: Pages
        public async Task<IActionResult> Index(int? id)//int? id
        {
            var pages = _context.pages.Include(p => p.book);
            if (id == null)
            {
            
                return View(await pages
                .ToListAsync());
               
            }
            else
            {
                ViewData["id"] = id;
                //ViewData["colour"] = await journalDbContext.Where(p => p.bookId == id);
                var page = await pages
                                .Where(p => p.bookId == id)
                                .ToListAsync();
                
                return View(page);
            }
            //var journalDbContext = _context.pages.Include(p => p.book);
            
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.pages
                .Include(p => p.book)
                .FirstOrDefaultAsync(m => m.id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create(int id)
        {
            ViewData["bookId"] = new SelectList(_context.books, "id", "title",id);
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("title,pageNum,content,bookId,dateCreated,dateUpdated")] Page page, int id)
        {
            if (ModelState.IsValid)
            {
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = id });
            }
            ViewData["bookId"] = new SelectList(_context.books, "id", "title", page.bookId);
            return View(page);
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            ViewData["bookId"] = new SelectList(_context.books, "id", "title", page.bookId);
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,pageNum,content,bookId,dateCreated,dateUpdated")] Page page)
        {
            if (id != page.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.id))
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
            ViewData["bookId"] = new SelectList(_context.books, "id", "title", page.bookId);
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.pages
                .Include(p => p.book)
                .FirstOrDefaultAsync(m => m.id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.pages.FindAsync(id);
            _context.pages.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _context.pages.Any(e => e.id == id);
        }
    }
}
