using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblMrmaster
{
    public int Id { get; set; }

    public int BpCode { get; set; }

    public string BpName { get; set; } = null!;

    public string? Name { get; set; }

    public string? Position { get; set; }

    public double Nic { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string MobileNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string TshirtSize { get; set; } = null!;
}
