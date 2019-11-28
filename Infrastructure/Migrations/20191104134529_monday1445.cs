using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class monday1445 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorksiteId",
                table: "Worksites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionId",
                table: "Divisions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataEntryId",
                table: "DataEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorksiteId",
                table: "Worksites");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "DataEntryId",
                table: "DataEntries");
        }
    }
}
