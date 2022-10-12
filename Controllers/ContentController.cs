using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Controllers
{
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int programmeItemId)
        {
            Content content = await (from item in _context.Content
                                     where item.ProgrammeItems.Id == programmeItemId
                                     select new Content
                                     {
                                         Title = item.Title,
                                         VideoLink = item.VideoLink,
                                         HTMLContent = item.HTMLContent
                                     }).FirstOrDefaultAsync();

            return View(content);
        }
    }
}
