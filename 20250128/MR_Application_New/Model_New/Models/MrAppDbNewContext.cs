using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Model_New.Models;

public partial class MrAppDbNewContext : DbContext
{
    public MrAppDbNewContext()
    {
    }

    public MrAppDbNewContext(DbContextOptions<MrAppDbNewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MrReview> MrReviews { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<OutLetMasterDetail> OutLetMasterDetails { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<QuestionsNew> QuestionsNews { get; set; }

    public virtual DbSet<ReviewAnswer> ReviewAnswers { get; set; }

    public virtual DbSet<ReviewsPhoto> ReviewsPhotos { get; set; }

    public virtual DbSet<ShopDisplaySurvey> ShopDisplaySurveys { get; set; }

    public virtual DbSet<TblDepartment> TblDepartments { get; set; }

    public virtual DbSet<TblDistributor> TblDistributors { get; set; }

    public virtual DbSet<TblMrmaster> TblMrmasters { get; set; }

    public virtual DbSet<TblOfficeLocation> TblOfficeLocations { get; set; }

    public virtual DbSet<TblReviewAnswer> TblReviewAnswers { get; set; }

    public virtual DbSet<TblSalesRep> TblSalesReps { get; set; }

    public virtual DbSet<TblSystemUser> TblSystemUsers { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserType> TblUserTypes { get; set; }

    public virtual DbSet<TblUslworkLevel> TblUslworkLevels { get; set; }

    public virtual DbSet<UserQuestion> UserQuestions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BCBLR-VP3580;Database=MR_APP_DB_New;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263C6D6FD9BA");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MarkIn).HasColumnType("datetime");
            entity.Property(e => e.MarkOut).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attendanc__UserI__2645B050");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BF703F80D");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<MrReview>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__MR_Revie__D48250243572B5B8");

            entity.ToTable("MR_Review");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.Distributor).HasMaxLength(255);
            entity.Property(e => e.PartyHllcode).HasMaxLength(50);
            entity.Property(e => e.PartyMasterCode).HasMaxLength(50);
            entity.Property(e => e.PartyName).HasMaxLength(255);
            entity.Property(e => e.SubmittedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserQuestionId).HasColumnName("UserQuestionID");

            entity.HasOne(d => d.UserQuestion).WithMany(p => p.MrReviews)
                .HasForeignKey(d => d.UserQuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MR_Review__UserQ__67DE6983");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Options__92C7A1DFBD08DEAA");

            entity.Property(e => e.OptionId).HasColumnName("OptionID");
            entity.Property(e => e.Distributor).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OptionText).HasMaxLength(255);
            entity.Property(e => e.PartyHllcode).HasMaxLength(50);
            entity.Property(e => e.PartyMasterCode).HasMaxLength(50);
            entity.Property(e => e.PartyName).HasMaxLength(255);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Options__Questio__5F492382");
        });

        modelBuilder.Entity<OutLetMasterDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OutLetMa__3214EC0746EEAAAD");

            entity.ToTable("OutLetMaster_Details");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address3)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address4)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(18, 10)")
                .HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(18, 10)")
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.OlCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("OL_CREATED_DATE");
            entity.Property(e => e.ParStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PAR_STATUS");
            entity.Property(e => e.PartyHllcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PartyHLLCode");
            entity.Property(e => e.PartyMasterCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PartyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrimaryChannel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimarychannelCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Primarychannel_Code");
            entity.Property(e => e.RsName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RS_NAME");
            entity.Property(e => e.Rscode).HasColumnName("RSCODE");
            entity.Property(e => e.SecondaryChannel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SecondarychannelCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Secondarychannel_Code");
            entity.Property(e => e.UpdateStamp).HasColumnName("UPDATE_STAMP");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C3CE612FB");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Distributor).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OptionType).HasMaxLength(20);
            entity.Property(e => e.PartyHllcode).HasMaxLength(50);
            entity.Property(e => e.PartyMasterCode).HasMaxLength(50);
            entity.Property(e => e.PartyName).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Questions__Categ__589C25F3");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Question__92C7A1FF68718DA1");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__Quest__2057CCD0");
        });

        modelBuilder.Entity<QuestionsNew>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FACEE48D9DC");

            entity.ToTable("QuestionsNew");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReviewAnswer>(entity =>
        {
            entity.HasKey(e => e.ReviewAnswerId).HasName("PK__ReviewAn__B480D9B7F71AEDAD");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.Outlet).HasMaxLength(100);
            entity.Property(e => e.PhotoFilePath).HasMaxLength(255);
            entity.Property(e => e.Rscode).HasMaxLength(50);

            entity.HasOne(d => d.Question).WithMany(p => p.ReviewAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewAns__Quest__2704CA5F");
        });

        modelBuilder.Entity<ReviewsPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07D383965F");

            entity.ToTable("ReviewsPhoto");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmpNo).HasMaxLength(50);
            entity.Property(e => e.PhotoPath1).HasMaxLength(255);
            entity.Property(e => e.PhotoPath2).HasMaxLength(255);
            entity.Property(e => e.PhotoPath3).HasMaxLength(255);
            entity.Property(e => e.Question1).HasMaxLength(255);
            entity.Property(e => e.Question2).HasMaxLength(255);
            entity.Property(e => e.Question3).HasMaxLength(255);
            entity.Property(e => e.Rscode)
                .HasMaxLength(50)
                .HasColumnName("RSCode");
            entity.Property(e => e.SelectedOutlet).HasMaxLength(100);
        });

        modelBuilder.Entity<ShopDisplaySurvey>(entity =>
        {
            entity.HasKey(e => e.SurveyId).HasName("PK__ShopDisp__A5481F9D8DBF421B");

            entity.ToTable("ShopDisplaySurvey");

            entity.Property(e => e.SurveyId).HasColumnName("SurveyID");
            entity.Property(e => e.DisplayOption).HasMaxLength(50);
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Empno)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMPNO");
            entity.Property(e => e.PartyHllcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PartyHLLCode");
            entity.Property(e => e.PartyMasterCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PartyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RsName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RS_NAME");
            entity.Property(e => e.Rscid).HasColumnName("RSCID");
            entity.Property(e => e.Rscode).HasColumnName("RSCODE");
            entity.Property(e => e.SurveyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Rsc).WithMany(p => p.ShopDisplaySurveys)
                .HasForeignKey(d => d.Rscid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopDisplaySurvey_OutletMaster");

            entity.HasOne(d => d.User).WithMany(p => p.ShopDisplaySurveys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopDisplaySurvey_Salesperson");
        });

        modelBuilder.Entity<TblDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Tbl_Depa__B2079BEDB4296274");

            entity.ToTable("Tbl_Department");

            entity.Property(e => e.DepartmentName).HasMaxLength(255);
        });

        modelBuilder.Entity<TblDistributor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Dist__3214EC07E4640F3C");

            entity.ToTable("Tbl_Distributor");

            entity.Property(e => e.DistributorName)
                .HasMaxLength(255)
                .HasColumnName("Distributor_Name");
        });

        modelBuilder.Entity<TblMrmaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_MRMa__3214EC077CF0AE54");

            entity.ToTable("Tbl_MRMaster");

            entity.Property(e => e.BpCode).HasColumnName("BP_Code");
            entity.Property(e => e.BpName)
                .HasMaxLength(255)
                .HasColumnName("BP_Name");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasColumnName("EMail_Address");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .HasColumnName("Mobile_Number");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Nic).HasColumnName("NIC");
            entity.Property(e => e.Position).HasMaxLength(255);
            entity.Property(e => e.TshirtSize)
                .HasMaxLength(50)
                .HasColumnName("TShirt_Size");
        });

        modelBuilder.Entity<TblOfficeLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Tbl_Offi__E7FEA497DC008CF0");

            entity.ToTable("Tbl_OfficeLocation");

            entity.Property(e => e.LocationName).HasMaxLength(255);
        });

        modelBuilder.Entity<TblReviewAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Revi__3214EC0787BD8212");

            entity.ToTable("Tbl_ReviewAnswers");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EmpNo).HasMaxLength(255);
            entity.Property(e => e.Outlet).HasMaxLength(255);
            entity.Property(e => e.Rscode).HasMaxLength(255);
        });

        modelBuilder.Entity<TblSalesRep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_sale__3213E83F5105013B");

            entity.ToTable("Tbl_sales_rep");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(10)
                .HasColumnName("action");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.BpCode).HasColumnName("bp_code");
            entity.Property(e => e.JoinedDate)
                .HasMaxLength(255)
                .HasColumnName("joined_date");
            entity.Property(e => e.Nic)
                .HasMaxLength(255)
                .HasColumnName("nic");
            entity.Property(e => e.RepCode)
                .HasMaxLength(255)
                .HasColumnName("rep_code");
            entity.Property(e => e.RepName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("rep_name");
            entity.Property(e => e.UslReference)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usl_reference");
        });

        modelBuilder.Entity<TblSystemUser>(entity =>
        {
            entity.HasKey(e => e.SystemUserId).HasName("PK__Tbl_Syst__8788C295B9C08C11");

            entity.ToTable("Tbl_SystemUser");

            entity.HasIndex(e => e.EmpNo, "UQ_EmpNo").IsUnique();

            entity.Property(e => e.EmpNo).HasMaxLength(6);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.UserType).WithMany(p => p.TblSystemUsers)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserType");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tbl_User__1788CC4C9F12E7F0");

            entity.ToTable("Tbl_User");

            entity.Property(e => e.Designation).HasMaxLength(255);
            entity.Property(e => e.EmpEmail).HasMaxLength(255);
            entity.Property(e => e.EmpNo).HasMaxLength(10);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LineManager).HasMaxLength(255);
            entity.Property(e => e.LineManagerEmail).IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.SalGrade).HasMaxLength(10);

            entity.HasOne(d => d.Department).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_User__Depart__6477ECF3");

            entity.HasOne(d => d.Location).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_User__Locati__66603565");

            entity.HasOne(d => d.WorkLevel).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.WorkLevelId)
                .HasConstraintName("FK__Tbl_User__WorkLe__68487DD7");
        });

        modelBuilder.Entity<TblUserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK__Tbl_User__40D2D816ED87254C");

            entity.ToTable("Tbl_UserType");

            entity.Property(e => e.UserTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblUslworkLevel>(entity =>
        {
            entity.HasKey(e => e.Wlid).HasName("PK__Tbl_USLW__F973E5E277F59662");

            entity.ToTable("Tbl_USLWorkLevel");

            entity.Property(e => e.Wlid)
                .ValueGeneratedNever()
                .HasColumnName("WLId");
            entity.Property(e => e.Wl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("WL");
            entity.Property(e => e.Wlstatus).HasColumnName("WLStatus");
        });

        modelBuilder.Entity<UserQuestion>(entity =>
        {
            entity.HasKey(e => e.UserQuestionId).HasName("PK__UserQues__225F9E73C279763A");

            entity.Property(e => e.UserQuestionId).HasColumnName("UserQuestionID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Distributor).HasMaxLength(255);
            entity.Property(e => e.PartyHllcode).HasMaxLength(50);
            entity.Property(e => e.PartyMasterCode).HasMaxLength(50);
            entity.Property(e => e.PartyName).HasMaxLength(255);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Question).WithMany(p => p.UserQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__UserQuest__Quest__640DD89F");

            entity.HasOne(d => d.User).WithMany(p => p.UserQuestions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserQuest__UserI__6319B466");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
