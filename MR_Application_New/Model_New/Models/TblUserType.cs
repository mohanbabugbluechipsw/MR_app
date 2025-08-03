using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblUserType
{
    public int UserTypeId { get; set; }

    public string? UserTypeName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TblSystemUser> TblSystemUsers { get; set; } = new List<TblSystemUser>();
}
