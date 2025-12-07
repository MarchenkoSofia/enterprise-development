using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRental.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class AddVirtualNavigationProperties : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bikes_Models_ModelId",
            table: "Bikes");

        migrationBuilder.DropForeignKey(
            name: "FK_Rents_Bikes_BikeId",
            table: "Rents");

        migrationBuilder.DropForeignKey(
            name: "FK_Rents_Renters_RenterId",
            table: "Rents");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "Renters",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Renters",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "MiddleName",
            table: "Renters",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "LastName",
            table: "Renters",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<decimal>(
            name: "PricePerHour",
            table: "Models",
            type: "decimal(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)");

        migrationBuilder.AddForeignKey(
            name: "FK_Bikes_Models_ModelId",
            table: "Bikes",
            column: "ModelId",
            principalTable: "Models",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Rents_Bikes_BikeId",
            table: "Rents",
            column: "BikeId",
            principalTable: "Bikes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Rents_Renters_RenterId",
            table: "Rents",
            column: "RenterId",
            principalTable: "Renters",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Bikes_Models_ModelId",
            table: "Bikes");

        migrationBuilder.DropForeignKey(
            name: "FK_Rents_Bikes_BikeId",
            table: "Rents");

        migrationBuilder.DropForeignKey(
            name: "FK_Rents_Renters_RenterId",
            table: "Rents");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "Renters",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(20)",
            oldMaxLength: 20);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Renters",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<string>(
            name: "MiddleName",
            table: "Renters",
            type: "nvarchar(max)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "LastName",
            table: "Renters",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(100)",
            oldMaxLength: 100);

        migrationBuilder.AlterColumn<decimal>(
            name: "PricePerHour",
            table: "Models",
            type: "decimal(18,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(10,2)",
            oldPrecision: 10,
            oldScale: 2);

        migrationBuilder.AddForeignKey(
            name: "FK_Bikes_Models_ModelId",
            table: "Bikes",
            column: "ModelId",
            principalTable: "Models",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Rents_Bikes_BikeId",
            table: "Rents",
            column: "BikeId",
            principalTable: "Bikes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Rents_Renters_RenterId",
            table: "Rents",
            column: "RenterId",
            principalTable: "Renters",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
