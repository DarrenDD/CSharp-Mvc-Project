using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpringOceanTechnologiesIMS.Data;

[assembly: HostingStartup(typeof(SpringOceanTechnologiesIMS.Areas.Identity.IdentityHostingStartup))]
namespace SpringOceanTechnologiesIMS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
        }
    }
}