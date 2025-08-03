using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_New.Models;
using Newtonsoft.Json;

namespace MR_Application_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] string jsonData, [FromForm] List<IFormFile> files)
        {
            var RSCode = HttpContext.Session.GetString("RSCode");
            var EmpNo = HttpContext.Session.GetString("EmpNo");
            var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");

            if (string.IsNullOrEmpty(jsonData))
                return BadRequest("Invalid data received.");

            ReviewPlaneModel model = JsonConvert.DeserializeObject<ReviewPlaneModel>(jsonData);

            if (model == null || model.PreAnswers == null || model.PostAnswers == null)
                return BadRequest("Invalid data received.");

            // Process file uploads
            foreach (var answer in model.PreAnswers.Concat(model.PostAnswers))
            {
                if (!string.IsNullOrEmpty(answer.PhotoPath) && files.Any(f => f.FileName == answer.PhotoPath))
                {
                    var file = files.First(f => f.FileName == answer.PhotoPath);
                    var filePath = Path.Combine("wwwroot/uploads", file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    answer.PhotoPath = "/uploads/" + file.FileName; // Update path in database
                }
            }

            var reviewAnswers = new List<TblReviewAnswer>();



            foreach (var answer in model.PreAnswers.Concat(model.PostAnswers))
            {
                //var reviewAnswer = new ReviewAnswer
                //{
                //    QuestionId = answer.QuestionId,
                //    Type = answer.Type,
                //    Answer = answer.Answer,
                //    PhotoData = !string.IsNullOrEmpty(answer.Photo) ? Convert.FromBase64String(answer.Photo) : null,
                //    EmpNo = ,
                //    Rscode = RSCode,
                //    Outlet = SelectedOutlet,
                //    CreatedAt = DateTime.Now,
                //};

                //reviewAnswers.Add(reviewAnswer);
            }



            return Ok(new { success = true, message = "Data received successfully", data = model });
        }






        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview([FromForm] ReviewSubmitModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var preAnswers = model.PreAnswers;  // List of pre-visit answers
            var postAnswers = model.PostAnswers; // List of post-visit answers

            // Process the answers and files
            // Assuming you have a method to save the answers and files to your database
            try
            {
                foreach (var answer in preAnswers)
                {
                    // Save pre-visit answers to the database
                    // Handle file uploads if any
                }

                foreach (var answer in postAnswers)
                {
                    // Save post-visit answers to the database
                    // Handle file uploads if any
                }

                return Ok("Review submitted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }

    public class ReviewPlaneModel
    {
        public List<QuestionAnswerModel> PreAnswers { get; set; }
        public List<QuestionAnswerModel> PostAnswers { get; set; }
    }

    public class QuestionAnswerModel
    {
        public int QuestionId { get; set; }
        public string Type { get; set; }
        public string Answer { get; set; }
        public string PhotoPath { get; set; }
    }

    public class ReviewSubmitModel
    {
        public string MRCode { get; set; }
        public string RSCODE { get; set; }
        public string SelectedOutlet { get; set; }



        public List<AnswerModel> PreAnswers { get; set; }
        public List<AnswerModel> PostAnswers { get; set; }

        // Additional properties as needed
    }

    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string Type { get; set; }
        public string Answer { get; set; }
        public string PhotoPath { get; set; }
    }

}
