using Microsoft.EntityFrameworkCore.Migrations;

namespace DbHotel.Migrations
{
    public partial class added_table_with_prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomPrice",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "conveniences",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "ConveniencesPriceConvenience",
                table: "Rooms",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConveniencesPrices",
                columns: table => new
                {
                    Convenience = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConveniencesPrices", x => x.Convenience);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ConveniencesPriceConvenience",
                table: "Rooms",
                column: "ConveniencesPriceConvenience");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_ConveniencesPrices_ConveniencesPriceConvenience",
                table: "Rooms",
                column: "ConveniencesPriceConvenience",
                principalTable: "ConveniencesPrices",
                principalColumn: "Convenience",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_ConveniencesPrices_ConveniencesPriceConvenience",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "ConveniencesPrices");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ConveniencesPriceConvenience",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ConveniencesPriceConvenience",
                table: "Rooms");

            migrationBuilder.AddColumn<decimal>(
                name: "RoomPrice",
                table: "Rooms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "conveniences",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
