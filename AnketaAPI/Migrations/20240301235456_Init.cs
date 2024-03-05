using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnketaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogSurvey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogSurveyQuestion",
                columns: table => new
                {
                    CatalogSurveyId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogSurveyQuestion", x => new { x.CatalogSurveyId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_CatalogSurveyQuestion_CatalogSurvey_CatalogSurveyId",
                        column: x => x.CatalogSurveyId,
                        principalTable: "CatalogSurvey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogSurveyQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCatalogSurvery",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CatalogSurveyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCatalogSurvery", x => new { x.CatalogSurveyId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCatalogSurvery_CatalogSurvey_CatalogSurveyId",
                        column: x => x.CatalogSurveyId,
                        principalTable: "CatalogSurvey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCatalogSurvery_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogSurveyQuestion_QuestionId",
                table: "CatalogSurveyQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCatalogSurvery_UserId",
                table: "UserCatalogSurvery",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogSurveyQuestion");

            migrationBuilder.DropTable(
                name: "UserCatalogSurvery");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "CatalogSurvey");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
