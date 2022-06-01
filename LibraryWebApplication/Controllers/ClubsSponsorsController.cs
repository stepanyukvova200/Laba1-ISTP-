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
    public class ClubsSponsorsController : Controller
    {
        private readonly DBLibrary2Context _context;

        public ClubsSponsorsController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: ClubsSponsors
        public async Task<IActionResult> Index()
        {
            var dBLibrary2Context = _context.ClubsSponsors.Include(c => c.Club).Include(c => c.EdrpouNavigation);
            return View(await dBLibrary2Context.ToListAsync());
        }

        // GET: ClubsSponsors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubsSponsors == null)
            {
                return NotFound();
            }

            var clubsSponsor = await _context.ClubsSponsors
                .Include(c => c.Club)
                .Include(c => c.EdrpouNavigation)
                .FirstOrDefaultAsync(m => m.ClubSponsorId == id);
            if (clubsSponsor == null)
            {
                return NotFound();
            }

            return View(clubsSponsor);
        }

        // GET: ClubsSponsors/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "NameClub");
            ViewData["Edrpou"] = new SelectList(_context.Sponsors, "Edrpou", "NameSponsor");
            return View();
        }

        // POST: ClubsSponsors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Edrpou,ClubId,ClubSponsorId")] ClubsSponsor clubsSponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubsSponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "NameClub", clubsSponsor.ClubId);
            ViewData["Edrpou"] = new SelectList(_context.Sponsors, "Edrpou", "NameSponsor", clubsSponsor.Edrpou);
            return View(clubsSponsor);
        }

        // GET: ClubsSponsors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubsSponsors == null)
            {
                return NotFound();
            }

            var clubsSponsor = await _context.ClubsSponsors.FindAsync(id);
            if (clubsSponsor == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "NameClub", clubsSponsor.ClubId);
            ViewData["Edrpou"] = new SelectList(_context.Sponsors, "Edrpou", "NameSponsor", clubsSponsor.Edrpou);
            return View(clubsSponsor);
        }

        // POST: ClubsSponsors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Edrpou,ClubId,ClubSponsorId")] ClubsSponsor clubsSponsor)
        {
            if (id != clubsSponsor.ClubSponsorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubsSponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubsSponsorExists(clubsSponsor.ClubSponsorId))
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
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "NameClub", clubsSponsor.ClubId);
            ViewData["Edrpou"] = new SelectList(_context.Sponsors, "Edrpou", "NameSponsor", clubsSponsor.Edrpou);
            return View(clubsSponsor);
        }

        // GET: ClubsSponsors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClubsSponsors == null)
            {
                return NotFound();
            }

            var clubsSponsor = await _context.ClubsSponsors
                .Include(c => c.Club)
                .Include(c => c.EdrpouNavigation)
                .FirstOrDefaultAsync(m => m.ClubSponsorId == id);
            if (clubsSponsor == null)
            {
                return NotFound();
            }

            return View(clubsSponsor);
        }

        // POST: ClubsSponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubsSponsors == null)
            {
                return Problem("Entity set 'DBLibrary2Context.ClubsSponsors'  is null.");
            }
            var clubsSponsor = await _context.ClubsSponsors.FindAsync(id);
            if (clubsSponsor != null)
            {
                _context.ClubsSponsors.Remove(clubsSponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubsSponsorExists(int id)
        {
          return _context.ClubsSponsors.Any(e => e.ClubSponsorId == id);
        }
    }
}
