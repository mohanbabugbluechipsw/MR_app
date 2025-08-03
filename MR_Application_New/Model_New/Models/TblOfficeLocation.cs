using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblOfficeLocation
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
