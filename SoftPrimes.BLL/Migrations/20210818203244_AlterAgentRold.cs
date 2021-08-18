using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.BLL.Migrations
{
    public partial class AlterAgentRold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentRoles_AppRoles_RoleId1",
                table: "AgentRoles");

            migrationBuilder.DropIndex(
                name: "IX_AgentRoles_RoleId1",
                table: "AgentRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AgentRoles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AgentRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentRoles_RoleId",
                table: "AgentRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentRoles_AppRoles_RoleId",
                table: "AgentRoles",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentRoles_AppRoles_RoleId",
                table: "AgentRoles");

            migrationBuilder.DropIndex(
                name: "IX_AgentRoles_RoleId",
                table: "AgentRoles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AgentRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                table: "AgentRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentRoles_RoleId1",
                table: "AgentRoles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentRoles_AppRoles_RoleId1",
                table: "AgentRoles",
                column: "RoleId1",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
