using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;
using NUnit.Framework.Internal;

namespace LibraryWebApplication.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly DBLibrary2Context _context;

        public SponsorsController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: Sponsors
        public async Task<IActionResult> Index()
        {
            var dBLibrary2Context = _context.Sponsors.Include(s => s.Country);
            return View(await dBLibrary2Context.ToListAsync());
        }

        // GET: Sponsors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Edrpou == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: Sponsors/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.CountriesDirectories, "Id", "Country");
            return View();
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Edrpou,NameSponsor,SphereOfActivity,CountryId")] Sponsor sponsor)
        {      
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateException ex)
                {
                    if(ex != null)
                    {
                        ModelState.AddModelError("Edrpou", "Даний єдрпоу занятий, оберіть інший");
                        return BadRequest("Даний єдрпоу занятий, оберіть інший");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.CountriesDirectories, "Id", "Country", sponsor.CountryId);
            return View(sponsor);
        }

        // GET: Sponsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.CountriesDirectories, "Id", "Country", sponsor.CountryId);
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Edrpou,NameSponsor,SphereOfActivity,CountryId")] Sponsor sponsor)
        {
            if (id != sponsor.Edrpou)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.Edrpou))
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
            ViewData["CountryId"] = new SelectList(_context.CountriesDirectories, "Id", "Country", sponsor.CountryId);
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sponsors == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.Edrpou == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sponsors == null)
            {
                return Problem("Entity set 'DBLibrary2Context.Sponsors'  is null.");
            }
            var sponsor = await _context.Sponsors.FindAsync(id);
            if (sponsor != null)
            {
                _context.Sponsors.Remove(sponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorExists(int id)
        {
          return _context.Sponsors.Any(e => e.Edrpou == id);
        }
    }
}
