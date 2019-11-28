using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class monday1514 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataEntries_Divisions_DivisionId",
                table: "DataEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_DataEntries_Companies_companyId",
                table: "DataEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Worksites_Companies_CompanyId",
                table: "Worksites");

            migrationBuilder.DropIndex(
                name: "IX_Worksites_CompanyId",
                table: "Worksites");

            migrationBuilder.DropIndex(
                name: "IX_DataEntries_DivisionId",
                table: "DataEntries");

            migrationBuilder.DropIndex(
                name: "IX_DataEntries_companyId",
                table: "DataEntries");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Worksites");

            migrationBuilder.DropColumn(
                name: "WorksiteId",
                table: "Worksites");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "DataEntryId",
                table: "DataEntries");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "DataEntries");

            migrationBuilder.DropColumn(
                name: "companyId",
                table: "DataEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Worksites",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "DivisionId",
                table: "DataEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "companyId",
                table: "DataEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Worksites_CompanyId",
                table: "Worksites",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEntries_DivisionId",
                table: "DataEntries",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEntries_companyId",
                table: "DataEntries",
                column: "companyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataEntries_Divisions_DivisionId",
                table: "DataEntries",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataEntries_Companies_companyId",
                table: "DataEntries",
                column: "companyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worksites_Companies_CompanyId",
                table: "Worksites",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
