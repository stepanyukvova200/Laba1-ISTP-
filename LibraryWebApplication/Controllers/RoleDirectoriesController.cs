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
    public class RoleDirectoriesController : Controller
    {
        private readonly DBLibrary2Context _context;

        public RoleDirectoriesController(DBLibrary2Context context)
        {
            _context = context;
        }

        // GET: RoleDirectories
        public async Task<IActionResult> Index()
        {
              return View(await _context.RoleDirectories.ToListAsync());
        }

        // GET: RoleDirectories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoleDirectories == null)
            {
                return NotFound();
            }

            var roleDirectory = await _context.RoleDirectories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleDirectory == null)
            {
                return NotFound();
            }

            return View(roleDirectory);
        }

        // GET: RoleDirectories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleDirectories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Role")] RoleDirectory roleDirectory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleDirectory);
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
            return View(roleDirectory);
        }

        // GET: RoleDirectories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoleDirectories == null)
            {
                return NotFound();
            }

            var roleDirectory = await _context.RoleDirectories.FindAsync(id);
            if (roleDirectory == null)
            {
                return NotFound();
            }
            return View(roleDirectory);
        }

        // POST: RoleDirectories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Role")] RoleDirectory roleDirectory)
        {
            if (id != roleDirectory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleDirectory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleDirectoryExists(roleDirectory.Id))
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
            return View(roleDirectory);
        }

        // GET: RoleDirectories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoleDirectories == null)
            {
                return NotFound();
            }

            var roleDirectory = await _context.RoleDirectories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleDirectory == null)
            {
                return NotFound();
            }

            return View(roleDirectory);
        }

        // POST: RoleDirectories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoleDirectories == null)
            {
                return Problem("Entity set 'DBLibrary2Context.RoleDirectories'  is null.");
            }
            var roleDirectory = await _context.RoleDirectories.FindAsync(id);
            if (roleDirectory != null)
            {
                _context.RoleDirectories.Remove(roleDirectory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleDirectoryExists(int id)
        {
          return _context.RoleDirectories.Any(e => e.Id == id);
        }
    }
}
