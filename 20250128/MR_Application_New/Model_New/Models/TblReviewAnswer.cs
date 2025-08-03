using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class TblReviewAnswer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Type { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public byte[]? PhotoData { get; set; }

    public string EmpNo { get; set; } = null!;

    public string Rscode { get; set; } = null!;

    public string Outlet { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
