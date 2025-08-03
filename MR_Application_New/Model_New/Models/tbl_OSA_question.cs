using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class tbl_OSA_question
    {
        public int Id { get; set; }
        public string OutletType { get; set; }
        public string OutletSubType { get; set; }
        public string Area { get; set; }
        public string GroupName { get; set; }  // This is your category
        public string QuestionText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Automatically set to current time
        public bool IsActive { get; set; } = true; // Indicates if the question is active
        public int CreatedBy { get; set; } // User ID who created the question
    }
}
