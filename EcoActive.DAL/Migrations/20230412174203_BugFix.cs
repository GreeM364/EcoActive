using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoActive.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activists_User_UserId",
                table: "Activists");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryAdmins_User_UserId",
                table: "FactoryAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryEmployees_User_UserId",
                table: "FactoryEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "BaseUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseUsers",
                table: "BaseUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists",
                column: "UserId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryAdmins_BaseUsers_UserId",
                table: "FactoryAdmins",
                column: "UserId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryEmployees_BaseUsers_UserId",
                table: "FactoryEmployees",
                column: "UserId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryAdmins_BaseUsers_UserId",
                table: "FactoryAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_FactoryEmployees_BaseUsers_UserId",
                table: "FactoryEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseUsers",
                table: "BaseUsers");

            migrationBuilder.RenameTable(
                name: "BaseUsers",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activists_User_UserId",
                table: "Activists",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryAdmins_User_UserId",
                table: "FactoryAdmins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FactoryEmployees_User_UserId",
                table: "FactoryEmployees",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
