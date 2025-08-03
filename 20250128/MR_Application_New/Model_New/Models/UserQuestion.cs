using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class UserQuestion
{
    public int UserQuestionId { get; set; }

    public int? UserId { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Distributor { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public string? PartyMasterCode { get; set; }

    public virtual ICollection<MrReview> MrReviews { get; set; } = new List<MrReview>();

    public virtual Question? Question { get; set; }

    public virtual TblUser? User { get; set; }
}
