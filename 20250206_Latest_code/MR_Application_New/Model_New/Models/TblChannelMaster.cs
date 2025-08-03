using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class TblChannelMaster
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Master_Channel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Channel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sub_channel { get; set; }

        [Required]
        [MaxLength(50)]
        public string Element { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sub_element { get; set; }


    }


}
