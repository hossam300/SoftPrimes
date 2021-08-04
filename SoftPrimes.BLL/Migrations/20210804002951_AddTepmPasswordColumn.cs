using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.BLL.Migrations
{
    public partial class AddTepmPasswordColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TempPassword",
                schema: "dbo",
                table: "Agents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TempPassword",
                schema: "dbo",
                table: "Agents");
        }
    }
}
