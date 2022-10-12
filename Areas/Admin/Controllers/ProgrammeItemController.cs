using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;
using SpringOceanTechnologiesIMS.Extensions;



namespace SpringOceanTechnologiesIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProgrammeItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgrammeItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProgrammeItem
        public async Task<IActionResult> Index(int programmeId)
        {
            List<ProgrammeItem> list = await (from progItem in _context.ProgrammeItems
                                              join contentItem in _context.Content
                                              on progItem.Id equals contentItem.ProgrammeItems.Id
                                              into gj
                                              from subContent in gj.DefaultIfEmpty()
                                              where progItem.ProgrammeId == programmeId
                                              select new ProgrammeItem
                                              {
                                                  Id = progItem.Id,
                                                  Title = progItem.Title,
                                                  Description = progItem.Description,
                                                  DateTimeItemReleased = progItem.DateTimeItemReleased,
                                                  MediaTypeId = progItem.MediaTypeId,
                                                  ProgrammeId = programmeId,
                                                  ContentId = (subContent != null) ? subContent.Id : 0

                                              }).ToListAsync();


            ViewBag.ProgrammeId = programmeId;

            return View(list);
        }

        // GET: Admin/ProgrammeItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmeItem = await _context.ProgrammeItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programmeItem == null)
            {
                return NotFound();
            }

            return View(programmeItem);
        }

        // GET: Admin/ProgrammeItem/Create
        public async Task<IActionResult> Create(int programmeId)
        {
            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();

            ProgrammeItem programmeItem = new ProgrammeItem
            {
                ProgrammeId = programmeId,
                MediaTypes = mediaTypes.ConvertToSelectList(0)
            };


            return View(programmeItem);

        }

        // POST: Admin/ProgrammeItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProgrammeId,MediaTypeId,DateTimeItemReleased")] ProgrammeItem programmeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programmeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {programmeId =programmeItem.ProgrammeId });
            }

            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();
            programmeItem.MediaTypes = mediaTypes.ConvertToSelectList(programmeItem.MediaTypeId);



            return View(programmeItem);
        }

        // GET: Admin/ProgrammeItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<MediaType> mediaTypes = await _context.MediaType.ToListAsync();

            var programmeItem = await _context.ProgrammeItems.FindAsync(id);
            if (programmeItem == null)
            {
                return NotFound();
            }

            programmeItem.MediaTypes = mediaTypes.ConvertToSelectList(programmeItem.MediaTypeId);

            return View(programmeItem);
        }

        // POST: Admin/ProgrammeItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProgrammeId,MediaTypeId,DateTimeItemReleased")] ProgrammeItem programmeItem)
        {
            if (id != programmeItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programmeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgrammeItemExists(programmeItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {programmeId =programmeItem.ProgrammeId});
            }
            return View(programmeItem);
        }

        // GET: Admin/ProgrammeItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programmeItem = await _context.ProgrammeItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programmeItem == null)
            {
                return NotFound();
            }

            return View(programmeItem);
        }

        // POST: Admin/ProgrammeItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programmeItem = await _context.ProgrammeItems.FindAsync(id);
            _context.ProgrammeItems.Remove(programmeItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { programmeId = programmeItem.ProgrammeId });
        }

        private bool ProgrammeItemExists(int id)
        {
            return _context.ProgrammeItems.Any(e => e.Id == id);
        }
    }
}
