using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ESW_Shelter.Data.Migrations
{
    public partial class RoleAuthorizationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    NameFront = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentID);
                });
            /**
             * Importante nao mexer na ordem das coisas, se não têm-se que alterar os id's nos controladores 
             */
            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Users", "Utilizadores" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Roles", "Permissões" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "ProductType", "Tipos de Produto" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Product", "Produtos" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Donation", "Donativos" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "AnimalType", "Tipos de Animais" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "AnimalRace", "Raças de Animais" }
                );

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Animal", "Animais" }
                );
            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Name", "NameFront" },
                values: new[] { "Statistics", "Estatísticas" }
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Administrator" }
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleName" },
                values: new[] { "Client" }
                );
            /********************************************************************/
            migrationBuilder.CreateTable(
                name: "RoleAuthorization",
                columns: table => new
                {
                    RoleAuthorizationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleFK = table.Column<int>(nullable: false),
                    ComponentFK = table.Column<int>(nullable: false),
                    Create = table.Column<bool>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAuthorization", x => x.RoleAuthorizationID);
                });
            migrationBuilder.AddForeignKey(name: "FK_RoleAuthorizationRoles_RoleAuthorization_Roles",
                table: "RoleAuthorization",
                column: "RoleFK",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(name: "FK_RoleAuthorizationComponent_RoleAuthorization_Component",
                    table: "RoleAuthorization",
                    column: "ComponentFK",
                    principalTable: "Components",
                    principalColumn: "ComponentID",
                    onDelete: ReferentialAction.NoAction);

            
            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 1, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 2, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 3, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 4, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 5, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 6, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 7, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 8, true, true, true, true }
                );

            migrationBuilder.InsertData(
                table: "RoleAuthorization",
                columns: new[] { "RoleFK", "ComponentFK", "Create", "Read", "Update", "Delete" },
                values: new object[] { 1, 9, true, true, true, true }
                );
            /**********************************************************************/

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Name", "Password", "ConfirmedEmail", "RoleId", "City", "DateOfBirth", "Phone", "PostalCode", "Street", "customerId" },
                values: new object[] { "administrador@admin.pt", "Admin", "Admin-12", true, 1, "", DateTime.Now, "000000000", "0000-000", "", "" }
                );
                
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "RoleAuthorization");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Client" }
                );

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumns: new[] { "RoleName" },
                keyValues: new[] { "Administrator" }
                );

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
    }
}
