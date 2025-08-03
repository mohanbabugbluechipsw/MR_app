using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
