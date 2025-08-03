using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int UserId { get; set; }

    public DateTime? MarkIn { get; set; }

    public DateTime? MarkOut { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
