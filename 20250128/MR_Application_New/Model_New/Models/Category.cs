using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
