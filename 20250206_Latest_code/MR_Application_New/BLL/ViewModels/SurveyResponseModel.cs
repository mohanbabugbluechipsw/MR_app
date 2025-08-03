//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BLL.ViewModels
//{
//    public class SurveyResponseModel
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        public string RSCode { get; set; } // Stores the selected RS Code

//        [Required]
//        public int SalespersonID { get; set; } // Stores the selected Salesperson ID

//        [Required]
//        public string SalespersonName { get; set; } // Optional: Store Salesperson's name for reference

//        // Survey Questions
//        public bool Question1Response { get; set; } // True if checked (Yes), false if unchecked (No)
//        public bool Question2Response { get; set; }

//        // Add more questions as needed
//        // Example:
//        public bool FabricCleaningQuestion { get; set; }
//        public bool SkinCleaningQuestion { get; set; }

//        // Optional: Add metadata for tracking
//        public DateTime ResponseDate { get; set; } = DateTime.Now; // Automatically sets to the current date/time

//        // Optional: Fields for dynamically calculated progress or additional details
//        public double CompletionPercentage { get; set; } // Track user progress dynamically (if applicable)
//    }
//}
