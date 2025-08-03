using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class Option
{
    public int OptionId { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? Distributor { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public string? PartyMasterCode { get; set; }

    public virtual Question? Question { get; set; }
}
