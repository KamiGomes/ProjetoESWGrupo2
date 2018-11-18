using ESW_Shelter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESW_Shelter.Data.Migrations
{
    [DbContext(typeof(ShelterContext))]
    [Migration("CreateIdentitySchema")]
    partial class CreateIdentitySchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
            {
                b.Property<int>("UsersID").ValueGeneratedOnAdd();

                b.Property<string>("Email").HasMaxLength(256);

                b.Property<string>("Name").HasMaxLength(256);

                b.Property<string>("Password").HasMaxLength(12);

                //b.HasKey("Id");

                b.HasIndex("Email")
                    .HasName("EmailIndex");

                /*b.HasIndex("UserID")
                    .IsUnique()
                    .HasName("UserIDIndex")
                    .HasFilter("[UserID] IS NOT NULL");

                b.ToTable("Users");*/
            });

#pragma warning restore 612, 618
        }
    }
}
