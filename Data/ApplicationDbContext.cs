using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpringOceanTechnologiesIMS.Entities;

namespace SpringOceanTechnologiesIMS.Data
{
    
    public class ApplicationUser : IdentityUser
    {
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Address1 { get; set; }
        [StringLength(250)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string PostCode { get; set; }        
        public string ThumbnailImagePath { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<CentreUser> CentreUsers { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserProgramme> UserProgrammes { get; set; }
        


    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Centre> Centres { get; set; }
        public DbSet<CentreProgrammes> CentreProgrammes { get; set; }
        public DbSet<CentreUser> CentreUsers { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ProgrammeItem> ProgrammeItems { get; set; }
        public DbSet<UserProgramme> UserProgramme { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
