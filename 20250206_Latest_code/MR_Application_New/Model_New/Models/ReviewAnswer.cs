using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class ReviewAnswer
{
    public int ReviewAnswerId { get; set; }

    public int QuestionId { get; set; }

    public string Answer { get; set; } = null!;

    public string? SelectedOptions { get; set; }

    public string EmpNo { get; set; } = null!;

    public string Rscode { get; set; } = null!;

    public string Outlet { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? PhotoFilePath { get; set; }

    public virtual QuestionsNew Question { get; set; } = null!;
}
