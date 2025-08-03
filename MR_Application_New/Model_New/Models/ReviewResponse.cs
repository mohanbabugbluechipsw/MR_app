using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class ReviewResponse
    {
        [Key]
        public int Id { get; set; }
        public string Rscode { get; set; }
        public string MrCode { get; set; }
        public string Outlet { get; set; }
        public string OutletType { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
