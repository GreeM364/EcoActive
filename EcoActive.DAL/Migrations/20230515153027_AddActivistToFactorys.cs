using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoActive.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddActivistToFactorys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivistId",
                table: "Factories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factories_ActivistId",
                table: "Factories",
                column: "ActivistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories",
                column: "ActivistId",
                principalTable: "Activists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factories_Activists_ActivistId",
                table: "Factories");

            migrationBuilder.DropIndex(
                name: "IX_Factories_ActivistId",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "ActivistId",
                table: "Factories");
        }
    }
}
