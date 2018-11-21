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

                b.Property<bool>("ConfirmedEmail");

                b.Property<int>("UserInfoID");
                //b.HasKey("Id");

                b.HasIndex("Email")
                    .HasName("EmailIndex");

                /*b.HasIndex("UserID")
                    .IsUnique()
                    .HasName("UserIDIndex")
                    .HasFilter("[UserID] IS NOT NULL");
                    */
                b.ToTable("Users");
            });

            modelBuilder.Entity("MicrosoftAspNetCore.Identity.IdentityUserInfo<int>", b =>
            {
                b.Property<int>("UserInfoID").ValueGeneratedOnAdd();

                b.Property<string>("Street").HasMaxLength(100);

                b.Property<string>("PostalCode").HasMaxLength(10);

                b.Property<string>("City").HasMaxLength(40);

                b.Property<int>("Phone");

                b.Property<int>("AlternativePhone");

                b.Property<string>("AlternativeEmail").HasMaxLength(150);

                b.Property<string>("Facebook").HasMaxLength(150);

                b.Property<string>("Twitter").HasMaxLength(150);

                b.Property<string>("Tumblr").HasMaxLength(150);

                b.Property<string>("Website").HasMaxLength(150);

                b.Property<int>("UserID").IsRequired();


                b.HasKey("UserInfoID");

                b.HasIndex("Street")
                    .HasName("StreetIndex");

                b.HasIndex("PostalCode")
                    .HasName("PostalCodeIndex");

                b.HasIndex("City")
                    .HasName("CityIndex");
                /*b.HasIndex("UserID")
                    .IsUnique()
                    .HasName("UserIDIndex")
                    .HasFilter("[UserID] IS NOT NULL");

                b.ToTable("Users");*/

                b.ToTable("UsersInfo");
            }
            );


            modelBuilder.Entity("MicrosoftAspNetCore.Identity.IdentityRoles<int>", b =>
            {
                b.Property<int>("RoleID").ValueGeneratedOnAdd();

                b.Property<string>("Name").HasMaxLength(100);

                b.HasKey("RoleID");

                b.HasIndex("Name")
                    .HasName("NameIndex");

                /*b.HasIndex("UserID")
                    .IsUnique()
                    .HasName("UserIDIndex")
                    .HasFilter("[UserID] IS NOT NULL");

                b.ToTable("Users");*/

                b.ToTable("Roles");
            }
            );
            /*
            modelBuilder.Entity<UsersInfo>()
                .HasOne<Users>(u => u.User)
                .WithOne(ui => ui.UserInfo)
                .HasForeignKey<UsersInfo>(ui => ui.UserInfoID);*/
#pragma warning restore 612, 618
        }
    }
}
