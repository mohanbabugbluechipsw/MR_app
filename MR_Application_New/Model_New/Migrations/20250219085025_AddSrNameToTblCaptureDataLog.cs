using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class AddSrNameToTblCaptureDataLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SrCode",
                table: "tblcapturedatalog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrCodeName",
                table: "tblcapturedatalog",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SrCode",
                table: "tblcapturedatalog");

            migrationBuilder.DropColumn(
                name: "SrCodeName",
                table: "tblcapturedatalog");
        }
    }
}
