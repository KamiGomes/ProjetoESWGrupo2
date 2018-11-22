using Microsoft.EntityFrameworkCore.Migrations;

namespace ESW_Shelter.Data.Migrations
{
    public partial class seed_roles_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Client" }
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Volunteer" }
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Employee" }
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Administrator" }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Client" }
                );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Volunteer" }
                );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Employee" }
                );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Administrator" }
                );
        }
    }
}
