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
    public class BootcampsController : Controller
    {
        private readonly DBLibrary2Context _context;

        public BootcampsController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: Bootcamps
        public async Task<IActionResult> Index()
        {
            var dBLibrary2Context = _context.Bootcamps.Include(b => b.Club);
            return View(await dBLibrary2Context.ToListAsync());
        }

        // GET: Bootcamps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bootcamps == null)
            {
                return NotFound();
            }

            var bootcamp = await _context.Bootcamps
                .Include(b => b.Club)
                .FirstOrDefaultAsync(m => m.BootcampId == id);
            if (bootcamp == null)
            {
                return NotFound();
            }

            return View(bootcamp);
        }

        // GET: Bootcamps/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId");
            return View();
        }

        // POST: Bootcamps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BootcampId,Location,ConstructionType,ClubId")] Bootcamp bootcamp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bootcamp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", bootcamp.ClubId);
            return View(bootcamp);
        }

        // GET: Bootcamps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bootcamps == null)
            {
                return NotFound();
            }

            var bootcamp = await _context.Bootcamps.FindAsync(id);
            if (bootcamp == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", bootcamp.ClubId);
            return View(bootcamp);
        }

        // POST: Bootcamps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BootcampId,Location,ConstructionType,ClubId")] Bootcamp bootcamp)
        {
            if (id != bootcamp.BootcampId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bootcamp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BootcampExists(bootcamp.BootcampId))
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
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", bootcamp.ClubId);
            return View(bootcamp);
        }

        // GET: Bootcamps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bootcamps == null)
            {
                return NotFound();
            }

            var bootcamp = await _context.Bootcamps
                .Include(b => b.Club)
                .FirstOrDefaultAsync(m => m.BootcampId == id);
            if (bootcamp == null)
            {
                return NotFound();
            }

            return View(bootcamp);
        }

        // POST: Bootcamps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bootcamps == null)
            {
                return Problem("Entity set 'DBLibrary2Context.Bootcamps'  is null.");
            }
            var bootcamp = await _context.Bootcamps.FindAsync(id);
            if (bootcamp != null)
            {
                _context.Bootcamps.Remove(bootcamp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BootcampExists(int id)
        {
          return _context.Bootcamps.Any(e => e.BootcampId == id);
        }
    }
}
