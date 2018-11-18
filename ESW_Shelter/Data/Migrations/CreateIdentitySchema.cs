using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Password =  table.Column<string>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
            migrationBuilder.DropTable(
                name: "UsersInfo");
        }
    }
}
