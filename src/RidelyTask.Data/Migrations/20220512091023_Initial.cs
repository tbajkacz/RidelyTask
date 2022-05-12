using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RidelyTask.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitorsRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Visitors = table.Column<long>(type: "bigint", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorsRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorsRecords");
        }
    }
}
