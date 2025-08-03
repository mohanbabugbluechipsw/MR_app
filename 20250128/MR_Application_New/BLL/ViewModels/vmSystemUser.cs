using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class vmSystemUser
    {
        public vmSystemUser()
        {

        }

        public int SystemUserId { get; set; }
        public string EmpNo { get; set; }

        public int UserTypeId { get; set; }

        public string UserType { get; set; }

        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string UserName { get; set; }
        public string NameWithTitle { get; set; }

        public int InHistory { get; set; }
        public string Email { get; set; }




    }



    public class vmUserTypes
    {
        public int UserTypeId { get; set; }
        public string UserType { get; set; }


    }
}
