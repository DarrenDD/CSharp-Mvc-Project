using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;
using SpringOceanTechnologiesIMS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ProgrammeItemDetailsModel> programmeItemDetailsModels = null;
            IEnumerable<GroupedProgrammeItemsByProgrammeModel> groupedProgrammeItemsByProgrammeModels = null;

            ProgrammeDetailsModel programmeDetailsModel = new ProgrammeDetailsModel();

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    programmeItemDetailsModels = await GetProgrammeItemDetailsForUser(user.Id);
                    groupedProgrammeItemsByProgrammeModels = GetGroupedProgrammeItemsByProgramme(programmeItemDetailsModels);

                    programmeDetailsModel.GroupedProgrammeItemsByProgrammeModels = groupedProgrammeItemsByProgrammeModels;

                }

            }
            else
            {
                var programmes = await GetProgrammesThatHaveContent();

                programmeDetailsModel.Programmes = programmes;

            }

            return View(programmeDetailsModel);
        }

        private async Task<List<Programme>> GetProgrammesThatHaveContent()
        {
            var programmesWithContent = await (from programme in _context.Programmes
                                              join programmeItem in _context.ProgrammeItems
                                               on programme.Id equals programmeItem.ProgrammeId
                                              join content in _context.Content
                                               on programmeItem.Id equals content.ProgrammeItems.Id
                                               select new Programme
                                               {
                                                   Id = programme.Id,
                                                   Title = programme.Title,
                                                   Description = programme.Description,
                                                   ThumbnailImagePath = programme.ThumbnailImagePath
                                               }).Distinct().ToListAsync();
            return programmesWithContent;

        }

        private IEnumerable<GroupedProgrammeItemsByProgrammeModel> GetGroupedProgrammeItemsByProgramme(IEnumerable<ProgrammeItemDetailsModel> programmeItemDetailsModels)
        {
            return from item in programmeItemDetailsModels
                   group item by item.ProgrammeId into g
                   select new GroupedProgrammeItemsByProgrammeModel
                   {
                       Id = g.Key,
                       Title = g.Select(c => c.ProgrammeTitle).FirstOrDefault(),
                       Items = g
                   };
        }

        private async Task<IEnumerable<ProgrammeItemDetailsModel>> GetProgrammeItemDetailsForUser(string userId)
        {
            return await (from progItem in _context.ProgrammeItems
                          join programme in _context.Programmes
                          on progItem.ProgrammeId equals programme.Id
                          join content in _context.Content
                          on progItem.Id equals content.ProgrammeItems.Id
                          join userProg in _context.UserProgramme
                          on programme.Id equals userProg.ProgrammeId
                          join mediaType in _context.MediaType
                          on progItem.MediaTypeId equals mediaType.Id
                          where userProg.UserId == userId
                          select new ProgrammeItemDetailsModel
                          {
                              ProgrammeId = programme.Id,
                              ProgrammeTitle = programme.Title,
                              ProgrammeItemId = progItem.Id,
                              ProgrammeItemTitle =progItem.Title,
                              ProgrammeItemDescription = progItem.Description,
                              MediaImagePath = mediaType.ThumbnailImagePath
                          }).ToListAsync();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
