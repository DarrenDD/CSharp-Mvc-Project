using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Areas.Admin.Models;
using SpringOceanTechnologiesIMS.Data;
using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersToProgrammeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataFunctions _dataFunctions;

        public UsersToProgrammeController(ApplicationDbContext context, IDataFunctions dataFunctions)
        {
            _context = context;
            _dataFunctions = dataFunctions;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersForProgramme(int programmeId)
        {
            UsersProgrammeListModel usersProgrammeListModel = new UsersProgrammeListModel();

            var allUsers = await GetAllUsers();
            var selectedUsersForProgramme = await GetSavedSelectedUsersForProgramme(programmeId);

            usersProgrammeListModel.Users = allUsers;
            usersProgrammeListModel.UsersSelected = selectedUsersForProgramme;

            return PartialView("_UsersListViewPartial", usersProgrammeListModel);

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Programmes.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers([Bind("ProgrammeId, UsersSelected")] UsersProgrammeListModel usersProgrammeListModel)
        {
            List<UserProgramme> usersSelectedForProgrammeToAdd = null;

            if (usersProgrammeListModel.UsersSelected != null)
            {
                usersSelectedForProgrammeToAdd = await GetUsersForProgrammeToAdd(usersProgrammeListModel);
            }

            var usersSelectedForProgrammeToDelete = await GetUsersForProgrammeToDelete(usersProgrammeListModel.ProgrammeId);

            await _dataFunctions.UpdateUserProgrammeEntityAsync(usersSelectedForProgrammeToDelete, usersSelectedForProgrammeToAdd);

            usersProgrammeListModel.Users = await GetAllUsers();

            return PartialView("_UsersListViewPartial", usersProgrammeListModel);

        }





        private async Task<List<UserModel>> GetAllUsers()
        {
            var allUsers = await (from user in _context.Users
                                  select new UserModel
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName
                                  }
                                  ).ToListAsync();
            return allUsers;
        }

        private async Task<List<UserProgramme>> GetUsersForProgrammeToAdd(UsersProgrammeListModel usersProgrammeListModel)
        {
            var usersForProgrammeToAdd = (from userProg in usersProgrammeListModel.UsersSelected
                                          select new UserProgramme
                                          {
                                              ProgrammeId = usersProgrammeListModel.ProgrammeId,
                                              UserId = userProg.Id
                                          }).ToList();

            return await Task.FromResult(usersForProgrammeToAdd);

        }
        private async Task<List<UserProgramme>> GetUsersForProgrammeToDelete(int programmeId)
        {
            var usersForProgrammeToDelete = await (from userProg in _context.UserProgramme
                                                   where userProg.ProgrammeId == programmeId
                                                   select new UserProgramme
                                                   {
                                                       Id = userProg.Id,
                                                       ProgrammeId = programmeId,
                                                       UserId = userProg.UserId
                                                   }
                                                  ).ToListAsync();
            return usersForProgrammeToDelete;

        }
        private async Task<List<UserModel>> GetSavedSelectedUsersForProgramme(int programmeId)
        {
            var savedSelectedUsersForProgramme = await (from usersToProg in _context.UserProgramme
                                                        where usersToProg.ProgrammeId == programmeId
                                                        select new UserModel
                                                        {
                                                            Id = usersToProg.UserId
                                                        }).ToListAsync();
            return savedSelectedUsersForProgramme;
        }
    }
}

