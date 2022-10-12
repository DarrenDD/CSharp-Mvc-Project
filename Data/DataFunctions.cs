using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Data
{
    public class DataFunctions : IDataFunctions
    {
        private readonly ApplicationDbContext _context;

        public DataFunctions(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateUserProgrammeEntityAsync(List<UserProgramme> userProgrammeItemsToDelete, List<UserProgramme> userProgrammeItemsToAdd)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    _context.RemoveRange(userProgrammeItemsToDelete);
                    await _context.SaveChangesAsync();

                    if (userProgrammeItemsToAdd != null)
                    {
                        _context.AddRange(userProgrammeItemsToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();

                }

                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }
        }
    }
}
