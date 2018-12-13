using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    }
}
