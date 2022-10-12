using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;

namespace SpringOceanTechnologiesIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CentreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CentreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Centre
        public async Task<IActionResult> Index()
        {
            return View(await _context.Centres.ToListAsync());
        }

        // GET: Admin/Centre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres
                .FirstOrDefaultAsync(m => m.CentreId == id);
            if (centre == null)
            {
                return NotFound();
            }

            return View(centre);
        }

        // GET: Admin/Centre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Centre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentreId,Name,Email,Address,CentrePhone,ThumbnailImagePath")] Centre centre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centre);
        }

        // GET: Admin/Centre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres.FindAsync(id);
            if (centre == null)
            {
                return NotFound();
            }
            return View(centre);
        }

        // POST: Admin/Centre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentreId,Name,Email,Address,CentrePhone,ThumbnailImagePath")] Centre centre)
        {
            if (id != centre.CentreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentreExists(centre.CentreId))
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
            return View(centre);
        }

        // GET: Admin/Centre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centre = await _context.Centres
                .FirstOrDefaultAsync(m => m.CentreId == id);
            if (centre == null)
            {
                return NotFound();
            }

            return View(centre);
        }

        // POST: Admin/Centre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centre = await _context.Centres.FindAsync(id);
            _context.Centres.Remove(centre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentreExists(int id)
        {
            return _context.Centres.Any(e => e.CentreId == id);
        }
    }
}
