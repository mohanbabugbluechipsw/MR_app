using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class QuestionsNew
{
    public int QuestionId { get; set; }

    public string Question { get; set; } = null!;

    public string Distributor { get; set; } = null!;

    public string PartyHllcode { get; set; } = null!;

    public string PartyName { get; set; } = null!;

    public string PartyMasterCode { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Status { get; set; }

    public bool Ba { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual ICollection<ReviewAnswer> ReviewAnswers { get; set; } = new List<ReviewAnswer>();
}
