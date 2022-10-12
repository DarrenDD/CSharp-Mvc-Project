using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;
using SpringOceanTechnologiesIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Controllers
{
    public class ProgrammesToUsersController : Controller
    {
        [Authorize]
        public class ProgrammesToUserController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IDataFunctions _dataFunctions;

            public ProgrammesToUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IDataFunctions dataFunctions)
            {
                _context = context;
                _userManager = userManager;
                _dataFunctions = dataFunctions;
            }
            public async Task<IActionResult> Index()
            {
                ProgrammesToUserModel programmesToUserModel = new ProgrammesToUserModel();

                var userId = _userManager.GetUserAsync(User).Result?.Id;

                programmesToUserModel.Programmes = await GetProgrammesThatHaveContent();

                programmesToUserModel.ProgrammesSelected = await GetProgrammesCurrentlySavedForUser(userId);

                programmesToUserModel.UserId = userId;

                return View(programmesToUserModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Index(string[] programmesSelected)
            {
                var userId = _userManager.GetUserAsync(User).Result?.Id;

                List<UserProgramme> userProgrammesToDelete = await GetProgrammesToDeleteForUser(userId);
                List<UserProgramme> userProgrammesToAdd = GetProgrammesToAddForUser(programmesSelected, userId);

                await _dataFunctions.UpdateUserProgrammeEntityAsync(userProgrammesToDelete, userProgrammesToAdd);

                return RedirectToAction("Index", "Home");
            }

            private async Task<List<Programme>> GetProgrammesThatHaveContent()
            {
                var programmesThatHaveContent = await (from programme in _context.Programmes
                                                       join programmeItem in _context.ProgrammeItems
                                                       on programme.Id equals programmeItem.ProgrammeId
                                                       join content in _context.Content
                                                       on programmeItem.Id equals content.ProgrammeItems.Id
                                                       select new Programme
                                                       {
                                                           Id = programme.Id,
                                                           Title = programme.Title,
                                                           Description = programme.Description
                                                       }).Distinct().ToListAsync();

                return programmesThatHaveContent;
            }
            private async Task<List<Programme>> GetProgrammesCurrentlySavedForUser(string userId)
            {
                var programmesCurrentlySavedForUser = await (from userProgramme in _context.UserProgramme
                                                             where userProgramme.UserId == userId
                                                             select new Programme
                                                             {
                                                                 Id = userProgramme.ProgrammeId,
                                                             }).ToListAsync();
                return programmesCurrentlySavedForUser;
            }
            private async Task<List<UserProgramme>> GetProgrammesToDeleteForUser(string userId)
            {
                var programmesToDelete = await (from userProg in _context.UserProgramme
                                                where userProg.UserId == userId
                                                select new UserProgramme
                                                {
                                                    Id = userProg.Id,
                                                    ProgrammeId = userProg.ProgrammeId,
                                                    UserId = userId
                                                }).ToListAsync();

                return programmesToDelete;
            }

            private List<UserProgramme> GetProgrammesToAddForUser(string[] programmesSelected, string userId)
            {
                var programmesToAdd = (from programmeId in programmesSelected
                                       select new UserProgramme
                                       {
                                           UserId = userId,
                                           ProgrammeId = int.Parse(programmeId)
                                       }).ToList();

                return programmesToAdd;

            }

        }
    }
}
