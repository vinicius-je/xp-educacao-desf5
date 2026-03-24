using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PitLaneShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PitLaneShopV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Carros",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Carros");
        }
    }
}
