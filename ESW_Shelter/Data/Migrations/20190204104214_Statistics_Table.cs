using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESW_Shelter.Data.Migrations
{
    public partial class Statistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekStock",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthStock",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoginStatistic",
                columns: table => new
                {
                    LoginStatisticID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateStatistic = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginStatistic", x => x.LoginStatisticID);
                });

            migrationBuilder.CreateTable(
                name: "RegisterStatistics",
                columns: table => new
                {
                    RegisterStatisticID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateStatistic = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterStatistics", x => x.RegisterStatisticID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginStatistic");

            migrationBuilder.DropTable(
                name: "RegisterStatistics");

            migrationBuilder.DropColumn(
                name: "MonthStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WeekStock",
                table: "Products");
        }
    }
}
