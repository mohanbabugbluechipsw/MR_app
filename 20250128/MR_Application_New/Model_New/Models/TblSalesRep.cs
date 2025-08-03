using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblSalesRep
{
    public int Id { get; set; }

    public int? BpCode { get; set; }

    public string? RepCode { get; set; }

    public string? RepName { get; set; }

    public string? JoinedDate { get; set; }

    public string? UslReference { get; set; }

    public string? Nic { get; set; }

    public int Active { get; set; }

    public string Action { get; set; } = null!;
}
