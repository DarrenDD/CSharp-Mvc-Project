using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Areas.Admin.Models
{
    public class UsersProgrammeListModel
    {
        public int ProgrammeId { get; set; }
        public ICollection<UserModel> Users { get; set; }

        public ICollection<UserModel> UsersSelected { get; set; }

    }
}
