using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Entities
{
    public class Centre
    {
        public int CentreId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string CentrePhone { get; set; }
        [Required]
        public string ThumbnailImagePath { get; set; }
        [ForeignKey("CentreId")]
        public virtual ICollection<CentreProgrammes> CentreProgrammes { get; set; }
        [ForeignKey("CentreId")]
        public virtual ICollection<CentreUser> CentreUsers { get; set; }
    }
}
