using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ESW_Shelter.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ESW_Shelter.Data.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmedEmail = table.Column<bool>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserID", x => x.UserID);
                }
            );

            migrationBuilder.CreateTable(
                name: "UsersInfo",
                columns: table => new
                {
                    UserInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: true),
                    AlternativePhone = table.Column<int>(nullable: true),
                    AlternativeEmail = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Twitter = table.Column<string>(nullable: true),
                    Instagram = table.Column<string>(nullable: true),
                    Tumblr = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoID", x => x.UserInfoID);
                    /*table.ForeignKey(
                        name: "FK_UserInfoID_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersID",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);*/
                }
            );

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
            });

            migrationBuilder.CreateIndex(
                name: "UsersByID",
                table: "Users",
                column: "UserID"
            );

            migrationBuilder.CreateIndex(
                name: "UsersInfoByID",
                table: "UsersInfo",
                column: "UserInfoID"
            );

            migrationBuilder.CreateIndex(
                name: "RolesByID",
                table: "Roles",
                column: "RoleID"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
            migrationBuilder.DropTable(
                name: "UsersInfo");
            migrationBuilder.DropTable(
                name: "Roles");
        }

        protected void Seed(ShelterContext shelterContext)
        {

            shelterContext.Roles.Add( 
                new Roles { RoleID = 1, RoleName = "Client" });
            shelterContext.Roles.Add(
                new Roles { RoleID = 2, RoleName = "Volunteer" });
            shelterContext.Roles.Add(
                new Roles { RoleID = 3, RoleName = "Employee" });
            shelterContext.Roles.Add(
                new Roles { RoleID = 4, RoleName = "Administrator" });

            /*roles.RoleName = "Client";
            roles.RoleName = "Volunteer";
            roles.RoleName = "Employee";
            roles.RoleName = "Administrator";*/
        }
    }
}
