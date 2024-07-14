using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Added_LeaveQuota_In_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaveQuotaId",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_LeaveQuotaId",
                table: "AbpUsers",
                column: "LeaveQuotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_LeaveQuotas_LeaveQuotaId",
                table: "AbpUsers",
                column: "LeaveQuotaId",
                principalTable: "LeaveQuotas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_LeaveQuotas_LeaveQuotaId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_LeaveQuotaId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LeaveQuotaId",
                table: "AbpUsers");
        }
    }
}
