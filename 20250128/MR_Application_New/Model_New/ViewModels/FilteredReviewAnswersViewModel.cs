using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.ViewModels
{
    public class FilteredReviewAnswersViewModel
    {
        public List<ReviewAnswerViewModel> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}
