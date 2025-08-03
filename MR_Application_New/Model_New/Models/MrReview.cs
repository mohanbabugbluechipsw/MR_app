using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class MrReview
{
    public int AnswerId { get; set; }

    public int? UserQuestionId { get; set; }

    public string? AnswerValue { get; set; }

    public string? Distributor { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public string? PartyMasterCode { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public virtual UserQuestion? UserQuestion { get; set; }
}
