using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Models
{
    public class ProgrammeItemDetailsModel
    {
        public int ProgrammeId { get; set; }
        public string ProgrammeTitle { get; set; }
        public int ProgrammeItemId { get; set; }
        public string ProgrammeItemTitle { get; set; }
        public string ProgrammeItemDescription { get; set; }
        public string MediaImagePath { get; set; }

    }
}
