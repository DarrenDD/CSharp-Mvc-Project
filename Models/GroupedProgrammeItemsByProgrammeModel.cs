using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Models
{
    public class GroupedProgrammeItemsByProgrammeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IGrouping<int, ProgrammeItemDetailsModel> Items { get; set; }
    }
}
