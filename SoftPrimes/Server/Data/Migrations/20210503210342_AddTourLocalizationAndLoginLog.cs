using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftPrimes.Server.Data.Migrations
{
    public partial class AddTourLocalizationAndLoginLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourCheckPoints_Tours_TourId",
                table: "TourCheckPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComments_Tours_TourId",
                table: "TourComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Agents_AgentId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_TourStateLogs_Tours_TourId",
                table: "TourStateLogs");

            migrationBuilder.DropIndex(
                name: "IX_Tours_AgentId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "EstimatedDistance",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "EstimatedEndDate",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourDate",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourState",
                table: "Tours");

            migrationBuilder.CreateTable(
                name: "TourAgents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TourDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AgentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TourState = table.Column<int>(type: "int", nullable: false),
                    EstimatedDistance = table.Column<double>(type: "float", nullable: false),
                    EstimatedEndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourAgents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourAgents_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "dbo",
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourAgents_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourAgents_AgentId",
                table: "TourAgents",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_TourAgents_TourId",
                table: "TourAgents",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourCheckPoints_TourAgents_TourId",
                table: "TourCheckPoints",
                column: "TourId",
                principalTable: "TourAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourComments_TourAgents_TourId",
                table: "TourComments",
                column: "TourId",
                principalTable: "TourAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourStateLogs_TourAgents_TourId",
                table: "TourStateLogs",
                column: "TourId",
                principalTable: "TourAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourCheckPoints_TourAgents_TourId",
                table: "TourCheckPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_TourComments_TourAgents_TourId",
                table: "TourComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TourStateLogs_TourAgents_TourId",
                table: "TourStateLogs");

            migrationBuilder.DropTable(
                name: "TourAgents");

            migrationBuilder.AddColumn<string>(
                name: "AgentId",
                table: "Tours",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EstimatedDistance",
                table: "Tours",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EstimatedEndDate",
                table: "Tours",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TourDate",
                table: "Tours",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "TourState",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_AgentId",
                table: "Tours",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourCheckPoints_Tours_TourId",
                table: "TourCheckPoints",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourComments_Tours_TourId",
                table: "TourComments",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Agents_AgentId",
                table: "Tours",
                column: "AgentId",
                principalSchema: "dbo",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourStateLogs_Tours_TourId",
                table: "TourStateLogs",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
