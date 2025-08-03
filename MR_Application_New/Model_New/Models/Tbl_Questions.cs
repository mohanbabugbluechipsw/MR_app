using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class Tbl_Questions
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(500)]
        public string Question { get; set; } = null!;

        [Required]
        public string Distributor { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public bool Status { get; set; }

        public bool Ba { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        [MaxLength(50)]
        public string Element { get; set; } = null!;  // New column


        public virtual ICollection<ReviewAnswer> ReviewAnswers { get; set; } = new List<ReviewAnswer>();
    }
}
