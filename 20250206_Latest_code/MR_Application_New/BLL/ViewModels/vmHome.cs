using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class vmHome
    {
        public vmHome()
        {
            
            lstUser = new List<vmUserDetails>();
        }
      
        public List<vmUserDetails> lstUser { get; set; }
    }
}
