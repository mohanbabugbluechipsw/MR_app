using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblUslworkLevel
{
    public int Wlid { get; set; }

    public string Wl { get; set; } = null!;

    public int Wlstatus { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
