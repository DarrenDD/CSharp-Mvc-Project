using SpringOceanTechnologiesIMS.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Entities
{
    public class Programme:IPrimaryProperties
    {
        public int Id { get;set; }
        [Required(ErrorMessage ="Hey!! This field is required mate")] 
        [StringLength(200,MinimumLength =2)]
        public string Title { get; set; }
        public string Description { get; set; } 
        [Required]
        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; }

        [ForeignKey ("ProgrammeId")]
        public virtual ICollection<ProgrammeItem> ProgrammeItems { get; set; }

        [ForeignKey("ProgrammeId")]
        public virtual ICollection<UserProgramme> UserProgrammes { get; set; }

        [ForeignKey("ProgrammeId")]
        public virtual ICollection<CentreProgrammes> CentreProgrammes { get; set; }

    }
}
