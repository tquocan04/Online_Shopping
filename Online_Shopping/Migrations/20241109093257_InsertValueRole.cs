using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class InsertValueRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11ae3454-c2a0-4b13-ae85-7e0063d1391f"), "STAFF" },
                    { new Guid("64442b7e-5955-469f-bbc9-2e6df052cb9c"), "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11ae3454-c2a0-4b13-ae85-7e0063d1391f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("64442b7e-5955-469f-bbc9-2e6df052cb9c"));
        }
    }
}
