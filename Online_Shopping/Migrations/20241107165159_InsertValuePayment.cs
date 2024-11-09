using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class InsertValuePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("5f750901-00ba-4658-99b1-17b6173e8ce6"), null, "ZaloPay" },
                    { new Guid("70d99884-b132-4913-9cc9-bf4b50885ec3"), null, "Momo" },
                    { new Guid("79ed495e-52c0-4446-a347-64c913fad40f"), null, "Cash" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("5f750901-00ba-4658-99b1-17b6173e8ce6"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("70d99884-b132-4913-9cc9-bf4b50885ec3"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: new Guid("79ed495e-52c0-4446-a347-64c913fad40f"));
        }
    }
}
