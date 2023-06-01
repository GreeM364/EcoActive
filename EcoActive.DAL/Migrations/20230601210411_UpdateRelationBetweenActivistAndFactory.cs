using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoActive.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationBetweenActivistAndFactory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists");

            migrationBuilder.DropForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories");

            migrationBuilder.AddForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists",
                column: "UserId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories",
                column: "ActivistId",
                principalTable: "Activists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists");

            migrationBuilder.DropForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories");

            migrationBuilder.AddForeignKey(
                name: "FK_Activists_BaseUsers_UserId",
                table: "Activists",
                column: "UserId",
                principalTable: "BaseUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories",
                column: "ActivistId",
                principalTable: "Activists",
                principalColumn: "Id");
        }
    }
}
