using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class vmUserDetails
    {

        public int UserId { get; set; }
        public string EmpNo { get; set; }
        public int PayeeId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public int UserTypeId { get; set; }
        public bool IsActive { get; set; }
    }
}
