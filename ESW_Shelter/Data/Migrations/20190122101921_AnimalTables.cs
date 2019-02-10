using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ESW_Shelter.Data.Migrations
{
    public partial class AnimalTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "AnimalRace",
                columns: table => new
                {
                    AnimalRaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalRace", x => x.AnimalRaceID);
                });
            /***********************************************************************************************/
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Disinfection = table.Column<bool>(nullable:false, defaultValue: false),
                    Neutered = table.Column<bool>(nullable: false, defaultValue: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    AnimalTypeFK = table.Column<int>(nullable: false),
                    AnimalRaceFK = table.Column<int>(nullable: false),
                    OwnerFK = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalID);
                });
            migrationBuilder.AddForeignKey(name: "FK_AnimalAnimalType_Animal_Type",
                table: "Animal",
                column: "AnimalTypeFK",
                principalTable: "AnimalTypes",
                principalColumn: "AnimalTypeID",
                onDelete: ReferentialAction.NoAction);
            migrationBuilder.AddForeignKey(name: "FK_AnimalAnimalRace_Animal_Race",
                table: "Animal",
                column: "AnimalRaceFK",
                principalTable: "AnimalRace",
                principalColumn: "AnimalRaceID",
                onDelete: ReferentialAction.NoAction);
            /***********************************************************************************************/
            migrationBuilder.CreateTable(
                name: "AnimalUsers",
                columns: table => new
                {
                    AnimalUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimalFK = table.Column<int>(nullable: false),
                    UsersFK = table.Column<int>(nullable: false)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_AnimalUsers", x => x.AnimalUserID);
                 }
                );

            migrationBuilder.AddForeignKey(name: "FK_AnimalUsersUsers_AnimalUsers_Users",
                    table: "AnimalUsers",
                    column: "UsersFK",
                    principalTable: "Users",
                    principalColumn: "UserID",
                    onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(name: "FK_AnimalUsers_AnimalUsers_Animal",
                    table: "AnimalUsers",
                    column: "AnimalFK",
                    principalTable: "Animal",
                    principalColumn: "AnimalID",
                    onDelete: ReferentialAction.NoAction);
            /********************************************************************************/
            migrationBuilder.CreateTable(
                name: "AnimalProduct",
                columns: table => new
                {
                    AnimalProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimalFK = table.Column<int>(nullable: false),
                    ProductFK = table.Column<int>(nullable: false)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_AnimalProduct", x => x.AnimalProductID);
                 }
                );

            migrationBuilder.AddForeignKey(name: "FK_AnimalProductAnimal_AnimalProduct_Animal",
                    table: "AnimalProduct",
                    column: "AnimalFK",
                    principalTable: "Animal",
                    principalColumn: "AnimalID",
                    onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(name: "FK_AnimalProductProduct_AnimalProduct_Product",
                    table: "AnimalProduct",
                    column: "ProductFK",
                    principalTable: "Products",
                    principalColumn: "ProductID",
                    onDelete: ReferentialAction.NoAction);
            /****************************************************/
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimalFK = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 1000, nullable: false),
                    Length = table.Column<long>(nullable: false),
                    FileName = table.Column<string>(maxLength: 4000, nullable: false),
                    ContentType = table.Column<string>(maxLength: 4000, nullable: false),
                    ContentDisposition = table.Column<string>(maxLength: 4000, nullable: false)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_ImageID", x => x.ImageID);
                 }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalUsers");

            migrationBuilder.DropTable(
                name: "AnimalProduct");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "AnimalRace");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
