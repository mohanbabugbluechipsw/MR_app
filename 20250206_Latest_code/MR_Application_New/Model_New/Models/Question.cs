using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? CategoryId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string OptionType { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int? BeforeAfter { get; set; }

    public string? Distributor { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public string? PartyMasterCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual ICollection<UserQuestion> UserQuestions { get; set; } = new List<UserQuestion>();
}
