using ESW_Shelter.Models;
using Microsoft.EntityFrameworkCore;

namespace ESW_Shelter.Models
{
    public class ShelterContext : DbContext
    {
        public ShelterContext (DbContextOptions<ShelterContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<DonationProduct> DonationProduct { get; set; }
        public DbSet<AnimalRace> AnimalRace { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<AnimalProduct> AnimalProduct { get; set; }
        public DbSet<AnimalUsers> AnimalUsers { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<RoleAuthorization> RoleAuthorization { get; set; }
    }
}
