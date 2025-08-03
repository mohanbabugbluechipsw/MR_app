using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class QuestionTableNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tbl_QuestionsQuestionId",
                table: "ReviewAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Ba = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Element = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAnswers_Tbl_QuestionsQuestionId",
                table: "ReviewAnswers",
                column: "Tbl_QuestionsQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewAnswers_tbl_Questions_Tbl_QuestionsQuestionId",
                table: "ReviewAnswers",
                column: "Tbl_QuestionsQuestionId",
                principalTable: "tbl_Questions",
                principalColumn: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewAnswers_tbl_Questions_Tbl_QuestionsQuestionId",
                table: "ReviewAnswers");

            migrationBuilder.DropTable(
                name: "tbl_Questions");

            migrationBuilder.DropIndex(
                name: "IX_ReviewAnswers_Tbl_QuestionsQuestionId",
                table: "ReviewAnswers");

            migrationBuilder.DropColumn(
                name: "Tbl_QuestionsQuestionId",
                table: "ReviewAnswers");
        }
    }
}
