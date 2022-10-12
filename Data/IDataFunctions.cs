using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Data
{
    public interface IDataFunctions
    {
        Task UpdateUserProgrammeEntityAsync(List<UserProgramme> userProgrammeItemsToDelete, List<UserProgramme> userProgrammeItemsToAdd);
    }
}
