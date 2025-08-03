using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class Tbl_SRMaster_dataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SRMaster_Datas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RS_Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Party_HUL_Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Child_Party = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Servicing_PLG = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Beat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salesperson = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SMN_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Employee_ID = table.Column<long>(type: "bigint", nullable: false),
                    Locality_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mon = table.Column<bool>(type: "bit", nullable: false),
                    Tue = table.Column<bool>(type: "bit", nullable: false),
                    Wed = table.Column<bool>(type: "bit", nullable: false),
                    Thu = table.Column<bool>(type: "bit", nullable: false),
                    Fri = table.Column<bool>(type: "bit", nullable: false),
                    Sat = table.Column<bool>(type: "bit", nullable: false),
                    Sun = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SRMaster_Datas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SRMaster_Datas");
        }
    }
}
