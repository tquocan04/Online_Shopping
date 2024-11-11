using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class insertIsDefaultField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "CusAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "CusAddresses");
        }
    }
}
