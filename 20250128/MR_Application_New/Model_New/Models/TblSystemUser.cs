using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblSystemUser
{
    public int SystemUserId { get; set; }

    public string EmpNo { get; set; } = null!;

    public int UserTypeId { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public int? InHistory { get; set; }

    public virtual TblUserType UserType { get; set; } = null!;
}
