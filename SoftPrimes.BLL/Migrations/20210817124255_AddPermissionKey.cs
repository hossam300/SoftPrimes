using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.BLL.Migrations
{
    public partial class AddPermissionKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermissionKey",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionKey",
                table: "Permissions");
        }
    }
}
