using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnketaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "CatalogSurveyQuestion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Answer",
                table: "CatalogSurveyQuestion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
