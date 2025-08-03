using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model_New.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2BF703F80D", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutLetMaster_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RSCODE = table.Column<int>(type: "int", nullable: true),
                    RS_NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartyHLLCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PrimaryChannel = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SecondaryChannel = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PAR_STATUS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UPDATE_STAMP = table.Column<TimeOnly>(type: "time", nullable: true),
                    OL_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address1 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address3 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address4 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LATITUDE = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    LONGITUDE = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Primarychannel_Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Secondarychannel_Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OutLetMa__3214EC0746EEAAAD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsNew",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distributor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyHllcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyMasterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Ba = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06FACEE48D9DC", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewsPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RSCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SelectedOutlet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Question1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Question2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Question3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhotoPath1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhotoPath2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhotoPath3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__3214EC07D383965F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Depa__B2079BEDB4296274", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Distributor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distributor = table.Column<int>(type: "int", nullable: false),
                    Distributor_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Dist__3214EC07E4640F3C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MRMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BP_Code = table.Column<int>(type: "int", nullable: false),
                    BP_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NIC = table.Column<double>(type: "float", nullable: false),
                    Date_Of_Birth = table.Column<DateOnly>(type: "date", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EMail_Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TShirt_Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_MRMa__3214EC077CF0AE54", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_OfficeLocation",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Offi__E7FEA497DC008CF0", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ReviewAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    EmpNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rscode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Outlet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Revi__3214EC0787BD8212", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_sales_rep",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    bp_code = table.Column<int>(type: "int", nullable: true),
                    rep_code = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    rep_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    joined_date = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    usl_reference = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    nic = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    active = table.Column<int>(type: "int", nullable: false),
                    action = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_sale__3213E83F5105013B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_User__40D2D816ED87254C", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_USLWorkLevel",
                columns: table => new
                {
                    WLId = table.Column<int>(type: "int", nullable: false),
                    WL = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    WLStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_USLW__F973E5E277F59662", x => x.WLId);
                });

            migrationBuilder.CreateTable(
                name: "tblChannelMasters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Master_Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sub_channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Element = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sub_element = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblChannelMasters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    BeforeAfter = table.Column<int>(type: "int", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyHllcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06F8C3CE612FB", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK__Questions__Categ__589C25F3",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__92C7A1FF68718DA1", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK__QuestionO__Quest__2057CCD0",
                        column: x => x.QuestionId,
                        principalTable: "QuestionsNew",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewAnswers",
                columns: table => new
                {
                    ReviewAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedOptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rscode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Outlet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    PhotoFilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReviewAn__B480D9B7F71AEDAD", x => x.ReviewAnswerId);
                    table.ForeignKey(
                        name: "FK__ReviewAns__Quest__2704CA5F",
                        column: x => x.QuestionId,
                        principalTable: "QuestionsNew",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SystemUser",
                columns: table => new
                {
                    SystemUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InHistory = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_Syst__8788C295B9C08C11", x => x.SystemUserId);
                    table.ForeignKey(
                        name: "FK_UserType",
                        column: x => x.UserTypeId,
                        principalTable: "Tbl_UserType",
                        principalColumn: "UserTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PayeeId = table.Column<int>(type: "int", nullable: true),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalGrade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LineManager = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    WorkLevelId = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    LineManagerEmail = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tbl_User__1788CC4C9F12E7F0", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Tbl_User__Depart__6477ECF3",
                        column: x => x.DepartmentId,
                        principalTable: "Tbl_Department",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK__Tbl_User__Locati__66603565",
                        column: x => x.LocationId,
                        principalTable: "Tbl_OfficeLocation",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK__Tbl_User__WorkLe__68487DD7",
                        column: x => x.WorkLevelId,
                        principalTable: "Tbl_USLWorkLevel",
                        principalColumn: "WLId");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: true),
                    OptionText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Distributor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyHllcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Options__92C7A1DFBD08DEAA", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK__Options__Questio__5F492382",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MarkIn = table.Column<DateTime>(type: "datetime", nullable: true),
                    MarkOut = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__8B69263C6D6FD9BA", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK__Attendanc__UserI__2645B050",
                        column: x => x.UserID,
                        principalTable: "Tbl_User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ShopDisplaySurvey",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EMPNO = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EmpEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SurveyDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsDisplayVisible = table.Column<bool>(type: "bit", nullable: true),
                    DisplayOption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisplayPhotoBefore = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DisplayPhotoAfter = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AreUnileverBrandsSeparate = table.Column<bool>(type: "bit", nullable: true),
                    AreBrandVariantsSeparate = table.Column<bool>(type: "bit", nullable: true),
                    AreNonUnileverBrandsBetween = table.Column<bool>(type: "bit", nullable: true),
                    IsUnileverShelfStripAvailable = table.Column<bool>(type: "bit", nullable: true),
                    IsArrangementAdequateBefore = table.Column<bool>(type: "bit", nullable: true),
                    MissingItemsBefore = table.Column<bool>(type: "bit", nullable: true),
                    NewProductsAddedAfter = table.Column<bool>(type: "bit", nullable: true),
                    RearrangementDoneAfter = table.Column<bool>(type: "bit", nullable: true),
                    EnhancedVisibilityAfter = table.Column<bool>(type: "bit", nullable: true),
                    RSCID = table.Column<int>(type: "int", nullable: false),
                    RSCODE = table.Column<int>(type: "int", nullable: true),
                    RS_NAME = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartyHLLCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ShopDisp__A5481F9D8DBF421B", x => x.SurveyID);
                    table.ForeignKey(
                        name: "FK_ShopDisplaySurvey_OutletMaster",
                        column: x => x.RSCID,
                        principalTable: "OutLetMaster_Details",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShopDisplaySurvey_Salesperson",
                        column: x => x.UserID,
                        principalTable: "Tbl_User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    UserQuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    QuestionID = table.Column<int>(type: "int", nullable: true),
                    AnswerValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Distributor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyHllcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserQues__225F9E73C279763A", x => x.UserQuestionID);
                    table.ForeignKey(
                        name: "FK__UserQuest__Quest__640DD89F",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                    table.ForeignKey(
                        name: "FK__UserQuest__UserI__6319B466",
                        column: x => x.UserID,
                        principalTable: "Tbl_User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MR_Review",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserQuestionID = table.Column<int>(type: "int", nullable: true),
                    AnswerValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distributor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyHllcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PartyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PartyMasterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MR_Revie__D48250243572B5B8", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK__MR_Review__UserQ__67DE6983",
                        column: x => x.UserQuestionID,
                        principalTable: "UserQuestions",
                        principalColumn: "UserQuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserID",
                table: "Attendance",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MR_Review_UserQuestionID",
                table: "MR_Review",
                column: "UserQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryID",
                table: "Questions",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAnswers_QuestionId",
                table: "ReviewAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopDisplaySurvey_RSCID",
                table: "ShopDisplaySurvey",
                column: "RSCID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopDisplaySurvey_UserID",
                table: "ShopDisplaySurvey",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SystemUser_UserTypeId",
                table: "Tbl_SystemUser",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_EmpNo",
                table: "Tbl_SystemUser",
                column: "EmpNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_User_DepartmentId",
                table: "Tbl_User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_User_LocationId",
                table: "Tbl_User",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_User_WorkLevelId",
                table: "Tbl_User",
                column: "WorkLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_QuestionID",
                table: "UserQuestions",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_UserID",
                table: "UserQuestions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MR_Review");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "ReviewAnswers");

            migrationBuilder.DropTable(
                name: "ReviewsPhoto");

            migrationBuilder.DropTable(
                name: "ShopDisplaySurvey");

            migrationBuilder.DropTable(
                name: "Tbl_Distributor");

            migrationBuilder.DropTable(
                name: "Tbl_MRMaster");

            migrationBuilder.DropTable(
                name: "Tbl_ReviewAnswers");

            migrationBuilder.DropTable(
                name: "Tbl_sales_rep");

            migrationBuilder.DropTable(
                name: "Tbl_SystemUser");

            migrationBuilder.DropTable(
                name: "tblChannelMasters");

            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.DropTable(
                name: "QuestionsNew");

            migrationBuilder.DropTable(
                name: "OutLetMaster_Details");

            migrationBuilder.DropTable(
                name: "Tbl_UserType");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tbl_User");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tbl_Department");

            migrationBuilder.DropTable(
                name: "Tbl_OfficeLocation");

            migrationBuilder.DropTable(
                name: "Tbl_USLWorkLevel");
        }
    }
}
