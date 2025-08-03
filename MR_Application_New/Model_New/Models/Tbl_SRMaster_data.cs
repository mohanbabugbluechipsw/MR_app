using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class Tbl_SRMaster_data
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Auto-increment primary key

        [Required]
        [MaxLength(10)]
        public string RS_Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Party_HUL_Code { get; set; }

        [Required]
        [MaxLength(20)]
        public string Child_Party { get; set; }

        [Required]
        [MaxLength(10)]
        public string Servicing_PLG { get; set; }

        [MaxLength(50)]
        public string Beat { get; set; }

        [MaxLength(20)]
        public string Salesperson { get; set; }

        [MaxLength(100)]
        public string SMN_Name { get; set; }

        [Required]
        public long Employee_ID { get; set; }

        [MaxLength(50)]
        public string Locality_Name { get; set; }

        public bool Mon { get; set; } = false;
        public bool Tue { get; set; } = false;
        public bool Wed { get; set; } = false;
        public bool Thu { get; set; } = false;
        public bool Fri { get; set; } = false;
        public bool Sat { get; set; } = false;
        public bool Sun { get; set; } = false;
    }
}
