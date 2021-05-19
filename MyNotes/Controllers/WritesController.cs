using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyNotes.Data;
using MyNotes.Models;

namespace MyNotes.Controllers
{
    public class WritesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WritesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Writes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Writes.ToListAsync());
        }

        // GET: Writes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writes = await _context.Writes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (writes == null)
            {
                return NotFound();
            }

            return View(writes);
        }

        // GET: Writes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Writes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Date")] Writes writes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(writes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(writes);
        }

        // GET: Writes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writes = await _context.Writes.FindAsync(id);
            if (writes == null)
            {
                return NotFound();
            }
            return View(writes);
        }

        // POST: Writes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Date")] Writes writes)
        {
            if (id != writes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(writes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WritesExists(writes.Id))
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
            return View(writes);
        }

        // GET: Writes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writes = await _context.Writes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (writes == null)
            {
                return NotFound();
            }

            return View(writes);
        }

        // POST: Writes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var writes = await _context.Writes.FindAsync(id);
            _context.Writes.Remove(writes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Showsearch()
        {
    

            return View();
        }
        [Authorize]
        public async Task<IActionResult> ShowsearchResults(string SearchPhrase)
        {


            return View("Index",await _context.Writes.Where(W=>W.Title.Contains(SearchPhrase)).ToListAsync());
        }
        private bool WritesExists(int id)
        {
            return _context.Writes.Any(e => e.Id == id);
        }
    }
}
