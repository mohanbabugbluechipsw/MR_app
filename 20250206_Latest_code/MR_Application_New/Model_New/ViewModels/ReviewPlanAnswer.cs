using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.ViewModels
{
    public class ReviewPlanAnswer
    {
        public int QuestionId { get; set; }
        public string Type { get; set; }
        public string Answer { get; set; }  // Can be null
        public string PhotoData { get; set; }  // Can be null
        public int EmpNo { get; set; }
        public string Rscode { get; set; }
        public string Outlet { get; set; }
        public string CreatedAt { get; set; }

    }


    public class ReviewPlanRequest
    {
        public List<ReviewPlanAnswer> Answers { get; set; }
    }
}
