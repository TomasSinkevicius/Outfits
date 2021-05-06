using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Outfits.Models;

namespace Outfits.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Outfits.Models.Product> Product { get; set; }
        public DbSet<Outfits.Models.Shop> Shop { get; set; }
        public DbSet<Outfits.Models.OutfitPost> OutfitPost { get; set; }
    }
}
