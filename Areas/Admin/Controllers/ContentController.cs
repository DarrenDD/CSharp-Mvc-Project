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

namespace SpringOceanTechnologiesIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Admin/Content/Create
        public IActionResult Create(int programmeItemId, int programmeId)
        {
            Content content = new Content
            {
                ProgrammeId = programmeId,
                ProgItemId = programmeItemId
            };

            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,ProgItemId,ProgrammeId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.ProgrammeItems = await _context.ProgrammeItems.FindAsync(content.ProgItemId);
                _context.Add(content);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "ProgrammeItem", new { programmeId = content.ProgrammeId });
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int programmeItemId, int programmeId)
        {
            if (programmeItemId == 0)
            {
                return NotFound();
            }

            var content = await _context.Content.SingleOrDefaultAsync(item => item.ProgrammeItems.Id == programmeItemId);

            content.ProgrammeId = programmeId;

            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,ProgrammeId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "ProgrammeItem", new { programmeId = content.ProgrammeId });
            }
            return View(content);
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }
    }
}
