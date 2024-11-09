using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataCity_District : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Hà Nội" },
                    { new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Hồ Chí Minh" },
                    { new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Đà Nẵng" }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CityId", "Name" },
                values: new object[,]
                {
                    { new Guid("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"), new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Hải Châu" },
                    { new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 1" },
                    { new Guid("9fc84eab-708a-49a2-b819-06d0629e560a"), new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"), "Hoàng Sa" },
                    { new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Cầu Giấy" },
                    { new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"), new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"), "Ba Đình" },
                    { new Guid("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 5" },
                    { new Guid("f9be5b5e-847d-47e9-847a-0150c4f608e1"), new Guid("6f624665-053e-45d2-8dd6-42baa124b481"), "Quận 10" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Districts_Name",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("2e8e7f13-ca6a-42c7-a1a0-fc5b4c872b3f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("53b0b29e-7b2c-4d4b-accc-f693ce746539"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9fc84eab-708a-49a2-b819-06d0629e560a"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("b999d3eb-a753-49f4-897f-4c37002e1302"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("bfb4be74-f5ed-4e67-94ba-7ee067a2098d"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("c893d9cf-7b2d-4ebc-9c65-3f78e0fce6bb"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("f9be5b5e-847d-47e9-847a-0150c4f608e1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1b84594b-aa7f-4a69-b54a-96cbf9b17c6e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6f624665-053e-45d2-8dd6-42baa124b481"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fc446281-359c-46ec-a2b9-bf9f26014f88"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
