using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class tblcapturedataloginmigaration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblcapturedatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rscode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MrCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outlet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutletType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapturedPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblcapturedatalog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblcapturedatalog");
        }
    }
}
