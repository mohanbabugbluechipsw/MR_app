using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class ReviewsPhoto
{
    public int Id { get; set; }

    public string Rscode { get; set; } = null!;

    public string EmpNo { get; set; } = null!;

    public string SelectedOutlet { get; set; } = null!;

    public string? Question1 { get; set; }

    public string? Question2 { get; set; }

    public string? Question3 { get; set; }

    public string? PhotoPath1 { get; set; }

    public string? PhotoPath2 { get; set; }

    public string? PhotoPath3 { get; set; }

    public DateTime? CreatedAt { get; set; }
}
