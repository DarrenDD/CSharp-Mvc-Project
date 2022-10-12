using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Models
{
    public class ProgrammeDetailsModel
    {
        public IEnumerable<GroupedProgrammeItemsByProgrammeModel> GroupedProgrammeItemsByProgrammeModels { get; set; }
        public IEnumerable<Programme> Programmes{ get; set; }
    }
}
