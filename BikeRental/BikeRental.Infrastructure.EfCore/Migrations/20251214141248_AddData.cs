using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeRental.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class AddData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Models",
            columns: new[] { "Id", "BikeType", "BikeWeight", "BrakeType", "MaxPassengerWeight", "ModelYear", "PricePerHour", "WheelSize" },
            values: new object[,]
            {
                { 1, 1, 13.800000190734863, "Disc hydraulic", 120.0, 2025, 7.50m, 29.0 },
                { 2, 2, 11.199999809265137, "Rim v-brake", 110.0, 2024, 8.20m, 27.5 },
                { 3, 0, 15.899999618530273, "Disc mechanical", 130.0, 2023, 5.90m, 26.0 },
                { 4, 3, 12.699999809265137, "Disc hydraulic", 125.0, 2025, 6.80m, 28.0 },
                { 5, 3, 10.399999618530273, "Rim", 100.0, 2022, 4.50m, 20.0 },
                { 6, 5, 9.8000001907348633, "Rim", 95.0, 2021, 3.90m, 24.0 },
                { 7, 1, 12.100000381469727, "Disc mechanical", 115.0, 2024, 6.20m, 27.5 },
                { 8, 4, 18.299999237060547, "Disc hydraulic", 140.0, 2023, 9.10m, 29.0 },
                { 9, 0, 8.8999996185302734, "Disc hydraulic", 105.0, 2025, 10.50m, 28.0 },
                { 10, 0, 16.5, "Drum", 135.0, 2022, 5.20m, 26.0 }
            });

        migrationBuilder.InsertData(
            table: "Renters",
            columns: new[] { "Id", "LastName", "MiddleName", "Name", "PhoneNumber" },
            values: new object[,]
            {
                { 1, "Kovalev", "Ilyich", "Dmitry", "+7 901 111-11-11" },
                { 2, "Egorova", "Antonovna", "Sofia", "+7 902 222-22-22" },
                { 3, "Leontiev", "Olegovich", "Maxim", "+7 903 333-33-33" },
                { 4, "Romanova", "Sergeevna", "Maria", "+7 904 444-44-44" },
                { 5, "Gusev", "Valerievich", "Igor", "+7 905 555-55-55" },
                { 6, "Frolova", "Alexandrovna", "Alena", "+7 906 666-66-66" },
                { 7, "Semenov", "Andreevich", "Pavel", "+7 907 777-77-77" },
                { 8, "Morozova", "Dmitrievna", "Ekaterina", "+7 908 888-88-88" },
                { 9, "Nazarov", "Petrovich", "Artur", "+7 909 999-99-99" },
                { 10, "Voronova", "Igorevna", "Olga", "+7 900 000-00-00" }
            });

        migrationBuilder.InsertData(
            table: "Bikes",
            columns: new[] { "Id", "Color", "ModelId", "SerialNumber" },
            values: new object[,]
            {
                { 1, "Black", 1, "202501001" },
                { 2, "Red", 2, "2024R01015" },
                { 3, "Blue", 3, "2023X03210" },
                { 4, "Olive", 4, "2025B05077" },
                { 5, "Yellow", 5, "2022G06342" },
                { 6, "White", 6, "2021W08908" },
                { 7, "Orange", 7, "2024O04556" },
                { 8, "Graphite", 8, "2023G09999" },
                { 9, "Silver", 9, "2025S01555" },
                { 10, "Turquoise", 10, "2022T12640" }
            });

        migrationBuilder.InsertData(
            table: "Rents",
            columns: new[] { "Id", "BikeId", "Duration", "RenterId", "StartTime" },
            values: new object[,]
            {
                { 1, 1, 2, 1, new DateTime(2025, 8, 2, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, 2, 1, 2, new DateTime(2025, 8, 3, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                { 3, 3, 2, 3, new DateTime(2025, 8, 5, 10, 15, 0, 0, DateTimeKind.Unspecified) },
                { 4, 4, 3, 4, new DateTime(2025, 8, 7, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, 5, 4, 5, new DateTime(2025, 8, 10, 11, 45, 0, 0, DateTimeKind.Unspecified) },
                { 6, 2, 5, 6, new DateTime(2025, 8, 12, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, 3, 1, 7, new DateTime(2025, 8, 14, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                { 8, 4, 2, 8, new DateTime(2025, 8, 16, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                { 9, 1, 3, 9, new DateTime(2025, 8, 18, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                { 10, 2, 4, 10, new DateTime(2025, 8, 20, 17, 0, 0, 0, DateTimeKind.Unspecified) }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Rents",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Bikes",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Renters",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Models",
            keyColumn: "Id",
            keyValue: 5);
    }
}
