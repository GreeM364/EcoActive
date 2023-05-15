using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoActive.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEnvironmentalAndCriticalIndicators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriticalIndicators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriticalIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CriticalIndicators_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalIndicators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AirTemperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelativeAirHumidity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntensityOfThermalRadiation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnvironmentalIndicators_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriticalIndicators_FactoryId",
                table: "CriticalIndicators",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalIndicators_FactoryId",
                table: "EnvironmentalIndicators",
                column: "FactoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriticalIndicators");

            migrationBuilder.DropTable(
                name: "EnvironmentalIndicators");
        }
    }
}
