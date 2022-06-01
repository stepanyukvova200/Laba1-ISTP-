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
    public class ClubsTournamentsController : Controller
    {
        private readonly DBLibrary2Context _context;

        public ClubsTournamentsController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: ClubsTournaments
        public async Task<IActionResult> Index()
        {
            var dBLibrary2Context = _context.ClubsTournaments.Include(c => c.Club).Include(c => c.Tournament);
            return View(await dBLibrary2Context.ToListAsync());
        }

        // GET: ClubsTournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubsTournaments == null)
            {
                return NotFound();
            }

            var clubsTournament = await _context.ClubsTournaments
                .Include(c => c.Club)
                .Include(c => c.Tournament)
                .FirstOrDefaultAsync(m => m.ClubTournamentId == id);
            if (clubsTournament == null)
            {
                return NotFound();
            }

            return View(clubsTournament);
        }

        // GET: ClubsTournaments/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Location");
            return View();
        }

        // POST: ClubsTournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TournamentId,ClubId,ClubTournamentId")] ClubsTournament clubsTournament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubsTournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubsTournament.ClubId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Location", clubsTournament.TournamentId);
            return View(clubsTournament);
        }

        // GET: ClubsTournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubsTournaments == null)
            {
                return NotFound();
            }

            var clubsTournament = await _context.ClubsTournaments.FindAsync(id);
            if (clubsTournament == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubsTournament.ClubId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Location", clubsTournament.TournamentId);
            return View(clubsTournament);
        }

        // POST: ClubsTournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TournamentId,ClubId,ClubTournamentId")] ClubsTournament clubsTournament)
        {
            if (id != clubsTournament.ClubTournamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubsTournament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubsTournamentExists(clubsTournament.ClubTournamentId))
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
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubsTournament.ClubId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "TournamentId", "Location", clubsTournament.TournamentId);
            return View(clubsTournament);
        }

        // GET: ClubsTournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClubsTournaments == null)
            {
                return NotFound();
            }

            var clubsTournament = await _context.ClubsTournaments
                .Include(c => c.Club)
                .Include(c => c.Tournament)
                .FirstOrDefaultAsync(m => m.ClubTournamentId == id);
            if (clubsTournament == null)
            {
                return NotFound();
            }

            return View(clubsTournament);
        }

        // POST: ClubsTournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubsTournaments == null)
            {
                return Problem("Entity set 'DBLibrary2Context.ClubsTournaments'  is null.");
            }
            var clubsTournament = await _context.ClubsTournaments.FindAsync(id);
            if (clubsTournament != null)
            {
                _context.ClubsTournaments.Remove(clubsTournament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubsTournamentExists(int id)
        {
          return _context.ClubsTournaments.Any(e => e.ClubTournamentId == id);
        }
    }
}
