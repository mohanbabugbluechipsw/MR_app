using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.ViewModels
{
    public class ReviewAnswerViewModel
    {
        public string Question { get; set; }
        public string Distributor { get; set; }
        public string PartyHllcode { get; set; }
        public string PartyMasterCode { get; set; }
        public string CombinedData { get; set; }  // Add this property
    }
}
