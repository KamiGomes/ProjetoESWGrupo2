using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESW_Shelter.Data.Migrations
{
    public partial class RemoveUsersInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersInfo");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Users",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Users",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmedEmail",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UsersInfo",
                columns: table => new
                {
                    UserInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlternativeEmail = table.Column<string>(nullable: true),
                    AlternativePhone = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    Facebook = table.Column<string>(maxLength: 150, nullable: true),
                    Instagram = table.Column<string>(maxLength: 150, nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Tumblr = table.Column<string>(maxLength: 150, nullable: true),
                    Twitter = table.Column<string>(maxLength: 150, nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    Website = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInfo", x => x.UserInfoID);
                });
        }
    }
}
