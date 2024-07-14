using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Added_Changes_In_LeaveCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Annual",
                table: "LeaveCategorys");

            migrationBuilder.DropColumn(
                name: "Casual",
                table: "LeaveCategorys");

            migrationBuilder.RenameColumn(
                name: "Sick",
                table: "LeaveCategorys",
                newName: "LeaveType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveType",
                table: "LeaveCategorys",
                newName: "Sick");

            migrationBuilder.AddColumn<string>(
                name: "Annual",
                table: "LeaveCategorys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Casual",
                table: "LeaveCategorys",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
