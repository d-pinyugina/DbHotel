using Microsoft.EntityFrameworkCore.Migrations;

namespace DbHotel.Migrations
{
    public partial class rollback_test_migration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "roomPrice",
                table: "Rooms",
                newName: "RoomPrice");

            migrationBuilder.RenameColumn(
                name: "roomNumber",
                table: "Rooms",
                newName: "RoomNumber");

            migrationBuilder.RenameColumn(
                name: "isRoomAvalible",
                table: "Rooms",
                newName: "IsRoomAvalible");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Rooms",
                newName: "Capacity");

            migrationBuilder.AddColumn<int>(
                name: "conveniences",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conveniences",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "RoomPrice",
                table: "Rooms",
                newName: "roomPrice");

            migrationBuilder.RenameColumn(
                name: "RoomNumber",
                table: "Rooms",
                newName: "roomNumber");

            migrationBuilder.RenameColumn(
                name: "IsRoomAvalible",
                table: "Rooms",
                newName: "isRoomAvalible");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Rooms",
                newName: "capacity");
        }
    }
}
