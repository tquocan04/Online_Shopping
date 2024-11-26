using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class DataAdmin1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "DistrictId", "ObjectId", "Street", "BranchId", "CustomerId", "EmployeeId", "IsDefault" },
                values: new object[] { new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"), new Guid("d099d28c-2361-4fe2-8bdd-d659fd8dfe04"), "111 Phan Đình Phùng", null, null, null, false });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BranchId", "Dob", "Email", "Gender", "Name", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("d099d28c-2361-4fe2-8bdd-d659fd8dfe04"), new Guid("c8fa4f9a-d745-4d0e-849c-08dd0d7ef1d7"), new DateOnly(2004, 1, 20), "admin1@gmail.com", "Nam", "Admin1", "admin1", "0939771198", "Admin", "admin1" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Name",
                table: "Employees",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Name",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumns: new[] { "DistrictId", "ObjectId", "Street" },
                keyValues: new object[] { new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"), new Guid("d099d28c-2361-4fe2-8bdd-d659fd8dfe04"), "111 Phan Đình Phùng" });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("d099d28c-2361-4fe2-8bdd-d659fd8dfe04"));

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
