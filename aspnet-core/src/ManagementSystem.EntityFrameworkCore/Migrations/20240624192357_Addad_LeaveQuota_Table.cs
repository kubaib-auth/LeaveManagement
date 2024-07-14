using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Addad_LeaveQuota_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveQuotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sick = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Casual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemaingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveQuotas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveQuotas");
        }
    }
}
