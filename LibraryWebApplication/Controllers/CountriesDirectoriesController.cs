using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class CountriesDirectoriesController : Controller
    {
        private readonly DBLibrary2Context _context;

        public CountriesDirectoriesController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: CountriesDirectories
        public async Task<IActionResult> Index()
        {
              return View(await _context.CountriesDirectories.ToListAsync());
        }

        // GET: CountriesDirectories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CountriesDirectories == null)
            {
                return NotFound();
            }

            var countriesDirectory = await _context.CountriesDirectories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countriesDirectory == null)
            {
                return NotFound();
            }

            return View(countriesDirectory);
        }

        // GET: CountriesDirectories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountriesDirectories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Country")] CountriesDirectory countriesDirectory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countriesDirectory);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (ex != null)
                    {
                        return BadRequest("Ця роль вже існує");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(countriesDirectory);
        }

        // GET: CountriesDirectories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CountriesDirectories == null)
            {
                return NotFound();
            }

            var countriesDirectory = await _context.CountriesDirectories.FindAsync(id);
            if (countriesDirectory == null)
            {
                return NotFound();
            }
            return View(countriesDirectory);
        }

        // POST: CountriesDirectories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country")] CountriesDirectory countriesDirectory)
        {
            if (id != countriesDirectory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countriesDirectory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountriesDirectoryExists(countriesDirectory.Id))
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
            return View(countriesDirectory);
        }

        // GET: CountriesDirectories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CountriesDirectories == null)
            {
                return NotFound();
            }

            var countriesDirectory = await _context.CountriesDirectories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countriesDirectory == null)
            {
                return NotFound();
            }

            return View(countriesDirectory);
        }

        // POST: CountriesDirectories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CountriesDirectories == null)
            {
                return Problem("Entity set 'DBLibrary2Context.CountriesDirectories'  is null.");
            }
            var countriesDirectory = await _context.CountriesDirectories.FindAsync(id);
            if (countriesDirectory != null)
            {
                _context.CountriesDirectories.Remove(countriesDirectory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesDirectoryExists(int id)
        {
          return _context.CountriesDirectories.Any(e => e.Id == id);
        }
    }
}
