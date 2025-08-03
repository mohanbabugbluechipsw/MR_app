using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model_New.ViewModels
{
    public class PreviousReviewAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string Type { get; set; }
        public string Answer { get; set; }

        public string Photo { get; set; } // Store file path or Base64 string
        public string EmpNo { get; set; } = null!;
        public string Rscode { get; set; } = null!;
        public string Outlet { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

