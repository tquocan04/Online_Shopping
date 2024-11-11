using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class removeCusIdInCus_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CusAddresses_Customers_CustomerId",
                table: "CusAddresses");

            migrationBuilder.DropIndex(
                name: "IX_CusAddresses_CustomerId",
                table: "CusAddresses");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CusAddresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "CusAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CusAddresses_CustomerId",
                table: "CusAddresses",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CusAddresses_Customers_CustomerId",
                table: "CusAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
