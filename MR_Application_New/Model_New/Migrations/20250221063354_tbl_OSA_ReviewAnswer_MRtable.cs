using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class tbl_OSA_ReviewAnswer_MRtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_OSA_ReviewAnswer_MR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutletType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RSCODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyHLLCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OSA_ReviewAnswer_MR", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_OSA_ReviewAnswer_MR");
        }
    }
}
