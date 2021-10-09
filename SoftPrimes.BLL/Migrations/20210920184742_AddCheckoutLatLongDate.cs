using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.BLL.Migrations
{
    public partial class AddCheckoutLatLongDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutDate",
                table: "TourCheckPoints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CheckoutLat",
                table: "TourCheckPoints",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CheckoutLong",
                table: "TourCheckPoints",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutDate",
                table: "TourAgents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CheckoutLat",
                table: "TourAgents",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CheckoutLong",
                table: "TourAgents",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutDate",
                table: "TourCheckPoints");

            migrationBuilder.DropColumn(
                name: "CheckoutLat",
                table: "TourCheckPoints");

            migrationBuilder.DropColumn(
                name: "CheckoutLong",
                table: "TourCheckPoints");

            migrationBuilder.DropColumn(
                name: "CheckoutDate",
                table: "TourAgents");

            migrationBuilder.DropColumn(
                name: "CheckoutLat",
                table: "TourAgents");

            migrationBuilder.DropColumn(
                name: "CheckoutLong",
                table: "TourAgents");
        }
    }
}
