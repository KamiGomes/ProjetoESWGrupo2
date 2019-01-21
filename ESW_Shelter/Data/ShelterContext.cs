using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ESW_Shelter.Models;

namespace ESW_Shelter.Models
{
    public class ShelterContext : DbContext
    {
        public ShelterContext (DbContextOptions<ShelterContext> options)
            : base(options)
        {
        }

        public DbSet<ESW_Shelter.Models.Users> Users { get; set; }
        public DbSet<ESW_Shelter.Models.Roles> Roles { get; set; }
        public DbSet<ESW_Shelter.Models.AnimalType> AnimalTypes { get; set; }
        public DbSet<ESW_Shelter.Models.ProductType> ProductTypes { get; set; }
        public DbSet<ESW_Shelter.Models.Product> Products { get; set; }
        public DbSet<ESW_Shelter.Models.Donation> Donation { get; set; }
        public DbSet<ESW_Shelter.Models.DonationProduct> DonationProduct { get; set; }
        public DbSet<ESW_Shelter.Models.AnimalRace> AnimalRace { get; set; }
    }
}
