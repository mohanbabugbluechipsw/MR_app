using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class Tbl_OSA_ReviewAnswer_MR
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int QuestionId { get; set; } // Reference to Question

        [Required]
        public string Answer { get; set; } // Answer text or image URL

        public string OutletType { get; set; } // Outlet type from session

        public string RSCODE { get; set; } // RS Code from session

        public string PartyHLLCode { get; set; } // Outlet Code

        [Required]
        public string EmpNo { get; set; } // Change from int UserId to string EmpNo

        public DateTime SubmittedDate { get; set; } = DateTime.Now; // Timestamp
    }
}
