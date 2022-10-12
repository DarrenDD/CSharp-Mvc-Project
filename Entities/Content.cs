using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpringOceanTechnologiesIMS.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Display(Name ="HTML Content")]
        public string HTMLContent { get; set; }
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }
        public ProgrammeItem ProgrammeItems { get; set; }
        [NotMapped]
        public int ProgItemId { get; set; }

        [NotMapped]
        public int ProgrammeId { get; set; }
    }
}
