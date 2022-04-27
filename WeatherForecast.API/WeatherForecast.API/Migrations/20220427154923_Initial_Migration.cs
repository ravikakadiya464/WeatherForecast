using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.API.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForecastDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Icon = table.Column<int>(type: "int", nullable: false),
                    IconPhrase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CloudCover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WindSpeedUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForecastDetails_UnitOfMeasures_WindSpeedUnitId",
                        column: x => x.WindSpeedUnitId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Temperatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinPhrase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPhrase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperatures_UnitOfMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationKey = table.Column<int>(type: "int", nullable: false),
                    ForecastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AirQuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SunRiseTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SunSetTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TemperatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_ForecastDetails_DayId",
                        column: x => x.DayId,
                        principalTable: "ForecastDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_ForecastDetails_NightId",
                        column: x => x.NightId,
                        principalTable: "ForecastDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_Temperatures_TemperatureId",
                        column: x => x.TemperatureId,
                        principalTable: "Temperatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UnitOfMeasures",
                columns: new[] { "Id", "Unit", "UnitType" },
                values: new object[,]
                {
                    { new Guid("0322957e-f005-4abd-850a-8e5967f51a9b"), "hPa", 11 },
                    { new Guid("0a94bb0c-51cd-4567-a86d-1a556a7e2fbf"), "F", 18 },
                    { new Guid("0c28d856-226d-4c45-ba2d-4a1d9b6cc8ce"), "%", 20 },
                    { new Guid("11cdaa58-b526-46ad-9fe6-efecd7a98faf"), "mi/h", 9 },
                    { new Guid("1877a605-990f-4d96-b731-b79cf1c46b63"), "kPa", 13 },
                    { new Guid("1a5e1135-9722-4567-a1bb-b767e6764b3e"), "km/h", 7 },
                    { new Guid("4c5a0334-adfb-496e-9ffd-fe2b86503805"), "f", 21 },
                    { new Guid("5a739bb1-02a6-4d0b-b55c-1fc358010d80"), "mbar", 14 },
                    { new Guid("63ffaf5b-241b-4b0a-8c56-6e01dc615b0d"), "int", 22 },
                    { new Guid("6847d1d6-602c-42f8-877b-47b853a93091"), "km", 6 },
                    { new Guid("729a9502-394f-47c2-b875-01a5cbe8fd60"), "psi", 16 },
                    { new Guid("802bc95b-e093-4b3a-8f72-a96210ca10ca"), "mi", 2 },
                    { new Guid("8fb77ca5-9d8b-4fc3-bd8f-6bb73496e908"), "K", 19 },
                    { new Guid("95798572-9c5b-4814-810b-23b8fadc82c5"), "ft", 0 },
                    { new Guid("a1f42d11-6d94-40ff-a287-2449733fbd61"), "m", 5 },
                    { new Guid("a4034a2b-9e20-4192-b141-dd6f8db3cba3"), "mmHg", 15 },
                    { new Guid("bb1a5bcb-3fa7-435c-878b-f8ffb3654704"), "mm", 3 },
                    { new Guid("c08fd973-c7f3-451b-9a1c-4b26df617453"), "cm", 4 },
                    { new Guid("dc659f0a-7177-4b39-9934-7473007ef457"), "in", 1 },
                    { new Guid("de6c3420-df57-4575-95cf-5111474125f5"), "C", 17 },
                    { new Guid("e60b187d-b32e-43a3-a275-f0534b68b81e"), "m/s", 10 },
                    { new Guid("ed75a444-059d-4b01-a4a4-1a0e1fb86034"), "Hg", 12 },
                    { new Guid("f8c555b0-a11a-47e5-8ab7-bd79f7b90c80"), "kt", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForecastDetails_WindSpeedUnitId",
                table: "ForecastDetails",
                column: "WindSpeedUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Temperatures_UnitMeasureId",
                table: "Temperatures",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_DayId",
                table: "WeatherForecasts",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_NightId",
                table: "WeatherForecasts",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_TemperatureId",
                table: "WeatherForecasts",
                column: "TemperatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "ForecastDetails");

            migrationBuilder.DropTable(
                name: "Temperatures");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures");
        }
    }
}
