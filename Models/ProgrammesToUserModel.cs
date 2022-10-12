using SpringOceanTechnologiesIMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Models
{
    public class ProgrammesToUserModel
    {
        public string UserId { get; set; }
        public ICollection<Programme> Programmes { get; set; }
        public ICollection<Programme> ProgrammesSelected { get; set; }
    }
}
