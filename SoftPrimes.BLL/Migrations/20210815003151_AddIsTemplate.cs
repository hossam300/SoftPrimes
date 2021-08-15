using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.BLL.Migrations
{
    public partial class AddIsTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTemplate",
                table: "TourAgents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTemplate",
                table: "TourAgents");
        }
    }
}
