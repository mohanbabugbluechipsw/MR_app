using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string EmpNo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? PayeeId { get; set; }

    public string? EmpName { get; set; }

    public string? EmpEmail { get; set; }

    public DateTime? HireDate { get; set; }

    public string? SalGrade { get; set; }

    public string? LineManager { get; set; }

    public string? CostCenter { get; set; }

    public string? Designation { get; set; }

    public int LocationId { get; set; }

    public int DepartmentId { get; set; }

    public bool? IsActive { get; set; }

    public int? WorkLevelId { get; set; }

    public string? Gender { get; set; }

    public string? LineManagerEmail { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual TblDepartment Department { get; set; } = null!;

    public virtual TblOfficeLocation Location { get; set; } = null!;

    public virtual ICollection<ShopDisplaySurvey> ShopDisplaySurveys { get; set; } = new List<ShopDisplaySurvey>();

    public virtual ICollection<UserQuestion> UserQuestions { get; set; } = new List<UserQuestion>();

    public virtual TblUslworkLevel? WorkLevel { get; set; }
}
