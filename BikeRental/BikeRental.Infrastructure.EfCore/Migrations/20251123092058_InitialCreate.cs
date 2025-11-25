using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRental.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Models",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                WheelSize = table.Column<double>(type: "float", nullable: true),
                MaxPassengerWeight = table.Column<double>(type: "float", nullable: true),
                BikeWeight = table.Column<double>(type: "float", nullable: true),
                BrakeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ModelYear = table.Column<int>(type: "int", nullable: true),
                PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                BikeType = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Models", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Renters",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Renters", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Bikes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ModelId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Bikes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Bikes_Models_ModelId",
                    column: x => x.ModelId,
                    principalTable: "Models",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Rents",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                Duration = table.Column<int>(type: "int", nullable: false),
                BikeId = table.Column<int>(type: "int", nullable: false),
                RenterId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rents", x => x.Id);
                table.ForeignKey(
                    name: "FK_Rents_Bikes_BikeId",
                    column: x => x.BikeId,
                    principalTable: "Bikes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Rents_Renters_RenterId",
                    column: x => x.RenterId,
                    principalTable: "Renters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Bikes_ModelId",
            table: "Bikes",
            column: "ModelId");

        migrationBuilder.CreateIndex(
            name: "IX_Renters_PhoneNumber",
            table: "Renters",
            column: "PhoneNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Rents_BikeId",
            table: "Rents",
            column: "BikeId");

        migrationBuilder.CreateIndex(
            name: "IX_Rents_RenterId",
            table: "Rents",
            column: "RenterId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Rents");

        migrationBuilder.DropTable(
            name: "Bikes");

        migrationBuilder.DropTable(
            name: "Renters");

        migrationBuilder.DropTable(
            name: "Models");
    }
}
