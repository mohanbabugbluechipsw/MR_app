using BLL.ViewModels;
using DAL.IRepositories;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using Model_New.Models;
using Model_New.ViewModels;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MR_Application_New.Controllers
{


    public class ReviewPlaneController : Controller
    {

        private readonly MrAppDbNewContext _locationContext;




        private readonly ILogger<ReviewPlaneController> _logger;




        public ReviewPlaneController(MrAppDbNewContext mylocationContext, ILogger<ReviewPlaneController> logger)
        {
            _locationContext = mylocationContext;

            _logger = logger;

        }


        public IActionResult Wizard()
        {
            return PartialView("~/Views/ReviewPlane/PartialViews/_Wizard.cshtml");
        }






        [HttpGet]
        public IActionResult GetRSCodes()
        {
            try
            {
                // Get the 'term' query parameter from the URL
                var term = HttpContext.Request.Query["term"].ToString();

                // Validate the term (optional but recommended)
                if (string.IsNullOrWhiteSpace(term))
                {
                    return BadRequest(new { message = "Search term is too short or invalid." });
                }

                // Fetch and filter RSCODEs based on the last two digits of Rscode
                var rsCodes = _locationContext.TblDistributors
                    .Where(r => !string.IsNullOrEmpty(r.DistributorName) &&
                                r.Distributor != null &&
                                r.Distributor.ToString().EndsWith(term)) // Check last 2 digits of Rscode
                    .Select(r => new
                    {
                        code = r.Distributor,
                        name = r.DistributorName
                    })
                    .Distinct()
                    .ToList();

                return Json(rsCodes);
            }
            catch (Exception ex)
            {
                // Log the error using ASP.NET Core's built-in logger
                _logger.LogError(ex, "Error fetching RS Codes");

                // Redirect to the custom error page
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult GetOutletsByRS(int rscode)
        {
            try
            {
                // Validate the RSCODE input
                if (rscode <= 0)
                {
                    _logger.LogWarning("Invalid RSCODE provided: {RSCODE}", rscode);
                    return RedirectToAction("Error", "Home", new { message = "Invalid RSCODE provided." });
                }

                // Fetch outlets based on the provided RSCODE
                var outlets = _locationContext.OutLetMasterDetails
                    .Where(o => o.Rscode == rscode)
                    .Select(o => new
                    {
                        o.PartyHllcode,
                        o.PartyName,
                        o.Address4
                    })
                    .ToList();

                // Check if outlets were found
                if (outlets == null || !outlets.Any())
                {
                    _logger.LogInformation("No outlets found for RSCODE: {RSCODE}", rscode);
                    return RedirectToAction("Error", "Home", new { message = "No outlets found for the given RSCODE." });
                }

                return Json(outlets);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while fetching outlets for RSCODE: {RSCODE}", rscode);

                // Redirect to Home/Error with the exception message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred. Please try again later." });
            }
        }



        [HttpGet]
        public IActionResult GetOutlets(string term, int rscode)
        {
            try
            {
                // Validate the RSCODE input
                if (rscode <= 0)
                {
                    _logger.LogWarning("Invalid RSCODE provided: {RSCODE}", rscode);
                    return BadRequest(new { message = "Invalid RSCODE provided." });
                }

                // Validate the term input (ensure it is not null or empty)
                if (string.IsNullOrEmpty(term))
                {
                    _logger.LogWarning("Search term is empty or null.");
                    return BadRequest(new { message = "Search term cannot be empty." });
                }

                // Log the search parameters
                _logger.LogInformation("Fetching outlets for RSCODE: {RSCODE} with search term: {Term}", rscode, term);

                // Fetch outlets based on the provided RSCODE and search term
                var outlets = _locationContext.OutLetMasterDetails
                    .Where(o => o.Rscode == rscode &&
                            (o.PartyHllcode.Contains(term) || o.PartyName.ToString().Contains(term)))
                    .Select(o => new
                    {
                        code = o.PartyHllcode,
                        name = o.PartyName
                    })
                    .ToList();

                // Check if outlets were found
                if (outlets == null || !outlets.Any())
                {
                    _logger.LogInformation("No outlets found for RSCODE: {RSCODE} with search term: {Term}", rscode, term);
                    return NotFound(new { message = "No outlets found matching the given criteria." });
                }

                // Log the number of outlets found
                _logger.LogInformation("{OutletsCount} outlets found for RSCODE: {RSCODE} with search term: {Term}", outlets.Count, rscode, term);

                return Json(outlets);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while fetching outlets for RSCODE: {RSCODE} and search term: {Term}", rscode, term);

                // Redirect to home/error page with error message as query parameter
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred. Please try again later." });
            }
        }










        public IActionResult Index()
        {





            return View();

        }


        [HttpGet]
        public async Task<IActionResult> Getdetails()
        {
            var questions = await _locationContext.ReviewAnswers.ToListAsync();
            return PartialView("~/Views/ReviewPlane/PartialViews/_GetDetails.cshtml", questions); // Return the Partial View
        }



        //[HttpGet]
        //public async Task<IActionResult> GetTblSalesRep()
        //{
        //    var slaesrep = await _locationContext.Tbl_sales_reps.ToListAsync(); // Ensure it's converting to a list
        //    return PartialView("~/Views/ReviewPlane/PartialViews/_GetTblSalesRep.cshtml", slaesrep); // Return the Partial View
        //}






        [HttpGet]
        public async Task<IActionResult> GetPhotoDetails()
        {
            var questions = await _locationContext.ReviewsPhotos.ToListAsync(); // Ensure it's converting to a list
            return PartialView("~/Views/ReviewPlane/PartialViews/_GetPhotoDetails.cshtml", questions); // Return the Partial View
        }



        public IActionResult AddAfterReview()
        {
            try
            {
                // Log that the method execution started
                _logger.LogInformation("Executing AddbeforeReview action.");

                // Check user role and add to ViewBag
                //if (User.IsInRole("Users"))
                //{
                //    ViewBag.IsUsers = true;
                //}
                //else
                //{
                //    ViewBag.IsUsers = false;
                //}

                // Fetch the distributor list (RSList)
                var rsList = _locationContext.TblDistributors
                    .Select(o => new { o.Distributor, o.DistributorName })
                    .Distinct()
                    .ToList();

                // Validate if RSList contains data
                if (rsList == null || !rsList.Any())
                {
                    _logger.LogWarning("No RS codes found in TblDistributors.");
                    ViewBag.RSList = new List<object>();  // Empty list to prevent null reference
                }
                else
                {
                    // Log the number of distributors fetched
                    _logger.LogInformation("{RSListCount} RS codes fetched from TblDistributors.", rsList.Count);
                    ViewBag.RSList = rsList;
                }

                // Return the partial view for AddbeforeReview
                return PartialView("~/Views/ReviewPlane/PartialViews/_ReviewPlane.cshtml");
            }
            catch (Exception ex)
            {
                // Log the error with detailed information
                _logger.LogError(ex, "An error occurred while executing the AddbeforeReview action.");

                // Redirect to the home error page with a message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while fetching data. Please try again later." });
            }
        }




        [HttpPost]
        public IActionResult AddAfterReview(string selectedRscode, string mrcode, string selectedOutlet)
        {
            try
            {
                // Validate input parameters
                if (string.IsNullOrEmpty(selectedRscode) || string.IsNullOrEmpty(mrcode) || string.IsNullOrEmpty(selectedOutlet))
                {
                    _logger.LogWarning("Missing required parameters: selectedRscode, mrcode, or selectedOutlet.");
                    ModelState.AddModelError(string.Empty, "All fields are required.");
                    return RedirectToAction("Error", "Home", new { message = "Missing required parameters: selectedRscode, mrcode, or selectedOutlet." });
                }

                // Fetch the employee details based on the 'mrcode' (employee email or username)
                var employee = _locationContext.TblUsers
                    .Where(e => e.EmpEmail == mrcode || e.EmpName == mrcode)
                    .Select(e => new
                    {
                        EmpId = e.UserId,
                        EmpName = e.EmpName,
                        EmpEmail = e.EmpEmail
                    })
                    .FirstOrDefault();

                // Handle the case where no employee is found
                if (employee == null)
                {
                    _logger.LogWarning("Employee not found for mrcode: {Mrcode}", mrcode);
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    return RedirectToAction("Error", "Home", new { message = "Employee not found." });
                }

                // Extract the Rscode (assuming the format is "RSCode - SomeDescription")
                var rscode = selectedRscode.Split(" - ")[0];

                // Store the selected data in session
                HttpContext.Session.SetString("RSCode", rscode);
                HttpContext.Session.SetString("EmpNo", employee.EmpId.ToString());
                HttpContext.Session.SetString("SelectedOutlet", selectedOutlet);

                // Log the successful data capture
                _logger.LogInformation("Successfully stored data in session: Rscode={Rscode}, EmpNo={EmpNo}, SelectedOutlet={SelectedOutlet}", rscode, employee.EmpId, selectedOutlet);

                // Redirect to the next action (e.g., 'create')
                return RedirectToAction("AfterCreate");
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while processing AddbeforeReview for mrcode: {Mrcode}", mrcode);

                // Redirect to the home error page with a message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred. Please try again later." });
            }
        }





























        //[HttpPost]
        //public IActionResult AddbeforeReview(string selectedRscode, string mrcode, string selectedOutlet, int progress)
        //{
        //    try
        //    {
        //        // Validate input parameters
        //        if (string.IsNullOrEmpty(selectedRscode) || string.IsNullOrEmpty(mrcode) || string.IsNullOrEmpty(selectedOutlet))
        //        {
        //            _logger.LogWarning("Missing required parameters: selectedRscode, mrcode, or selectedOutlet.");
        //            ModelState.AddModelError(string.Empty, "All fields are required.");
        //            return RedirectToAction("Error", "Home", new { message = "Missing required parameters: selectedRscode, mrcode, or selectedOutlet." });
        //        }

        //        // Fetch the employee details based on the 'mrcode' (employee email or username)
        //        var employee = _locationContext.TblUsers
        //            .Where(e => e.EmpEmail == mrcode || e.EmpName == mrcode)
        //            .Select(e => new
        //            {
        //                EmpId = e.UserId,
        //                EmpName = e.EmpName,
        //                EmpEmail = e.EmpEmail
        //            })
        //            .FirstOrDefault();

        //        // Handle the case where no employee is found
        //        if (employee == null)
        //        {
        //            _logger.LogWarning("Employee not found for mrcode: {Mrcode}", mrcode);
        //            ModelState.AddModelError(string.Empty, "Employee not found.");
        //            return RedirectToAction("Error", "Home", new { message = "Employee not found." });
        //        }

        //        // Extract the Rscode (assuming the format is "RSCode - SomeDescription")
        //        var rscode = selectedRscode.Split(" - ")[0];

        //        // Store the selected data in session
        //        HttpContext.Session.SetString("RSCode", rscode);
        //        HttpContext.Session.SetString("EmpNo", employee.EmpId.ToString());
        //        HttpContext.Session.SetString("SelectedOutlet", selectedOutlet);

        //        // Log the successful data capture
        //        _logger.LogInformation("Successfully stored data in session: Rscode={Rscode}, EmpNo={EmpNo}, SelectedOutlet={SelectedOutlet}", rscode, employee.EmpId, selectedOutlet);

        //        // Calculate the progress based on the selected fields
        //        // For example: Add 20% for each step completed
        //        progress = 0;

        //        // Check if each field is selected and update the progress accordingly
        //        if (!string.IsNullOrEmpty(selectedRscode)) progress += 2;  // Add 20% for RSCODE
        //        if (!string.IsNullOrEmpty(mrcode)) progress += 2;        // Add 20% for MR Code
        //        if (!string.IsNullOrEmpty(selectedOutlet)) progress += 2;  // Add 20% for Outlet
        //                                                                   // You can add more checks for other fields if necessary
        //        HttpContext.Session.Set("progress", BitConverter.GetBytes(progress)); // Store progress as byte[]

        //        // Log the calculated progress
        //        _logger.LogInformation("Calculated Progress: {Progress}%", progress);

        //        // Redirect to the next action (e.g., 'create') and pass the progress value
        //        return RedirectToAction("Create", new { progress = progress });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while processing AddbeforeReview for mrcode: {Mrcode}", mrcode);

        //        // Redirect to the home error page with a message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred. Please try again later." });
        //    }
        //}


        //public async Task<IActionResult> Create(int progress)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");


        //        // Retrieve the progress value from the session
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert the byte array back to an int
        //        }


        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Missing session data: RSCode or outlet is null or empty.");
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log the session data for traceability
        //        _logger.LogInformation("Fetching questions for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Fetch questions based on RSCode and outlet
        //        var questions = await _locationContext.QuestionsNews
        //            .Include(q => q.QuestionOptions)  // Ensure options are loaded
        //            .Where(q => q.Distributor == RSCode && q.PartyHllcode == outlet && q.Ba == false && q.Status == true) // Add BA filter
        //            .ToListAsync();

        //        // If no questions were found, log the information
        //        if (questions == null || !questions.Any())
        //        {
        //            _logger.LogInformation("No questions found for RSCode: {RSCode}, Outlet: {Outlet}", RSCode, outlet);
        //            ViewBag.Message = "No questions found for the selected criteria.";
        //        }

        //        // Pass data to the view
        //        ViewBag.CurrentStep = progress;

        //        ViewBag.SysUsers = questions;
        //        ViewBag.Progress = progress; // Pass the progress value to the view

        //        return PartialView("~/Views/ReviewPlane/PartialViews/_Create.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while fetching questions for RSCode: {RSCode} and Outlet: {Outlet}",
        //            HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("SelectedOutlet"));

        //        // Redirect to the error page with a message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while loading the questions. Please try again later." });
        //    }
        //}
        //public async Task<IActionResult> Create(int progress)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Retrieve progress value from session
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert byte array to int
        //        }

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Missing session data: RSCode or outlet is null or empty.");
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log session data for traceability
        //        _logger.LogInformation("Fetching questions for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Fetch questions based on RSCode and outlet
        //        var questions = await _locationContext.QuestionsNews
        //            .Include(q => q.QuestionOptions)
        //            .Where(q => q.Distributor == RSCode && q.PartyHllcode == outlet && q.Ba == false && q.Status == true)
        //            .ToListAsync();

        //        // If no questions were found, log the information
        //        if (questions == null || !questions.Any())
        //        {
        //            _logger.LogInformation("No questions found for RSCode: {RSCode}, Outlet: {Outlet}", RSCode, outlet);
        //            ViewBag.Message = "No questions found for the selected criteria.";
        //        }

        //        // Load answers from session (if available)
        //        var savedAnswers = HttpContext.Session.GetString("answers");
        //        if (!string.IsNullOrEmpty(savedAnswers))
        //        {
        //            var deserializedAnswers = JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(savedAnswers); // Deserialize answers
        //            ViewBag.SavedAnswers = deserializedAnswers;  // Use this to pre-fill the form
        //        }

        //        // Pass data to the view
        //        ViewBag.CurrentStep = progress;
        //        ViewBag.SysUsers = questions;
        //        ViewBag.Progress = progress;

        //        return PartialView("~/Views/ReviewPlane/PartialViews/_Create.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while fetching questions for RSCode: {RSCode} and Outlet: {Outlet}", HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("SelectedOutlet"));
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while loading the questions. Please try again later." });
        //    }
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create(ReviewViewModel model)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Save answers in session to maintain state
        //        HttpContext.Session.SetString("answers", JsonConvert.SerializeObject(model.Answers)); // Serialize and store answers

        //        // Handle saving data (existing logic)
        //        foreach (var answer in model.Answers)
        //        {
        //            byte[] photoData = null;

        //            if (answer.PhotoPath != null && answer.PhotoPath.Length > 0)
        //            {
        //                // Process the photo (if it exists)
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await answer.PhotoPath.CopyToAsync(memoryStream);
        //                    photoData = memoryStream.ToArray();
        //                }

        //                var ReviewAnswer = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = photoData,
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(ReviewAnswer);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                var answerEntity = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = null,
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(answerEntity);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //        }

        //        // Update progress and session












        //        // Redirect to confirmation page
        //        return RedirectToAction("AfterCreate");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while saving answers for RSCode: {RSCode}, EmpNo: {EmpNo}");
        //        ModelState.AddModelError("", "An error occurred while saving your answers: " + ex.Message);
        //        return RedirectToAction("Error", "Home");
        //    }
        //}

        public IActionResult ReviewPlane()
        {
            try
            {
                // Log that the method execution started
                _logger.LogInformation("Executing AddbeforeReview action.");


                var rsList = _locationContext.TblDistributors
                    .Select(o => new { o.Distributor, o.DistributorName })
                    .Distinct()
                    .ToList();

                // Validate if RSList contains data
                if (rsList == null || !rsList.Any())
                {
                    _logger.LogWarning("No RS codes found in TblDistributors.");
                    ViewBag.RSList = new List<object>();  // Empty list to prevent null reference
                }
                else
                {
                    // Log the number of distributors fetched
                    _logger.LogInformation("{RSListCount} RS codes fetched from TblDistributors.", rsList.Count);
                    ViewBag.RSList = rsList;
                }

                // Return the partial view for AddbeforeReview
                return PartialView("~/Views/ReviewPlane/PartialViews/_ReviewPlane.cshtml");
            }
            catch (Exception ex)
            {
                // Log the error with detailed information
                _logger.LogError(ex, "An error occurred while executing the AddbeforeReview action.");

                // Redirect to the home error page with a message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while fetching data. Please try again later." });
            }
        }



        // In your ReviewPlaneController or appropriate controller

        [HttpGet]
        public async Task<IActionResult> GetQuestions(string rscode, string mrCode, string outlet)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(rscode) || string.IsNullOrEmpty(mrCode) || string.IsNullOrEmpty(outlet))
                {
                    return BadRequest("RSCODE, MRCode, and Outlet must be provided.");
                }

                // Find employee based on MRCode (either EmpEmail or EmpName)
                var employee = await _locationContext.TblUsers
                    .Where(e => e.EmpEmail == mrCode || e.EmpName == mrCode)
                    .Select(e => new { EmpId = e.UserId, EmpName = e.EmpName, EmpEmail = e.EmpEmail })
                    .FirstOrDefaultAsync();

                if (employee == null)
                {
                    _logger.LogWarning("Employee not found for mrcode: {Mrcode}", mrCode);
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    return BadRequest("Employee not found.");
                }

                // Split the outlet string to get the outlet code and name
                //var outletParts = outlet.Split(" - ");
                ////if (outletParts.Length != 2)
                ////{
                ////    return BadRequest("Invalid outlet format. Expected 'OutletName - OutletCode'.");
                ////}

                //string outletName = outletParts[0];
                //string outletCode = outletParts[1];

                // Check if there are any questions matching the provided criteria
                bool hasQuestions = await _locationContext.QuestionsNews
                    .AsNoTracking()
                    .AnyAsync(q => q.Distributor == rscode && q.PartyHllcode == outlet && !q.Ba && q.Status);

                List<QuestionsNew> questions = new List<QuestionsNew>();

                if (hasQuestions)
                {
                    // Fetch questions if they exist
                    questions = await _locationContext.QuestionsNews
                        .AsNoTracking()
                        .Where(q => q.Distributor == rscode && q.PartyHllcode == outlet && !q.Ba && q.Status)
                        .ToListAsync();
                }

                // Return questions as JSON
                return Json(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching questions for RSCODE: {rscode}, MRCode: {mrCode}, Outlet: {outlet}");
                return StatusCode(500, "An unexpected error occurred while fetching the questions.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> postQuestions(string rscode, string mrCode, string outlet)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(rscode) || string.IsNullOrEmpty(mrCode) || string.IsNullOrEmpty(outlet))
                {
                    return BadRequest("RSCODE, MRCode, and Outlet must be provided.");
                }

                // Find employee based on MRCode (either EmpEmail or EmpName)
                var employee = await _locationContext.TblUsers
                    .Where(e => e.EmpEmail == mrCode || e.EmpName == mrCode)
                    .Select(e => new { EmpId = e.UserId, EmpName = e.EmpName, EmpEmail = e.EmpEmail })
                    .FirstOrDefaultAsync();

                if (employee == null)
                {
                    _logger.LogWarning("Employee not found for mrcode: {Mrcode}", mrCode);
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    return BadRequest("Employee not found.");
                }

                // Splitting the outlet to get necessary information
                //var rscodeParts = outlet.Split(" - ");


                //string outletCode = rscodeParts[0];
                //string outletName = rscodeParts[1];

                // Check if there are any questions matching the provided criteria
                bool hasQuestions = await _locationContext.QuestionsNews
                    .AsNoTracking()
                    .AnyAsync(q => q.Distributor == rscode && q.PartyHllcode == outlet && q.Ba== true && q.Status == true);

                List<QuestionsNew> questions = new List<QuestionsNew>();

                if (hasQuestions)
                {
                    // Fetch questions if they exist
                    questions = await _locationContext.QuestionsNews
                        .AsNoTracking()
                        .Where(q => q.Distributor == rscode && q.PartyHllcode == outlet && q.Ba == true && q.Status == true)
                        .ToListAsync();
                }

                // Return questions as JSON
                return Json(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching questions for RSCODE: {rscode}, MRCode: {mrCode}, Outlet: {outlet}");
                return StatusCode(500, "An unexpected error occurred while fetching the questions.");
            }
        }


        public IActionResult AddbeforeReview()
        {
            try
            {
                // Log that the method execution started
                _logger.LogInformation("Executing AddbeforeReview action.");


                var rsList = _locationContext.TblDistributors
                    .Select(o => new { o.Distributor, o.DistributorName })
                    .Distinct()
                    .ToList();

                // Validate if RSList contains data
                if (rsList == null || !rsList.Any())
                {
                    _logger.LogWarning("No RS codes found in TblDistributors.");
                    ViewBag.RSList = new List<object>();  // Empty list to prevent null reference
                }
                else
                {
                    // Log the number of distributors fetched
                    _logger.LogInformation("{RSListCount} RS codes fetched from TblDistributors.", rsList.Count);
                    ViewBag.RSList = rsList;
                }

                // Return the partial view for AddbeforeReview
                return PartialView("~/Views/ReviewPlane/PartialViews/_ReviewPlane.cshtml");
            }
            catch (Exception ex)
            {
                // Log the error with detailed information
                _logger.LogError(ex, "An error occurred while executing the AddbeforeReview action.");

                // Redirect to the home error page with a message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while fetching data. Please try again later." });
            }
        }











        [HttpPost]
        public IActionResult AddbeforeReview(string selectedRscode, string mrcode, string selectedOutlet)
        {
            try
            {
                // Validate input parameters
                if (string.IsNullOrEmpty(selectedRscode) || string.IsNullOrEmpty(mrcode) || string.IsNullOrEmpty(selectedOutlet))
                {
                    _logger.LogWarning("Missing required parameters: selectedRscode, mrcode, or selectedOutlet.");
                    ModelState.AddModelError(string.Empty, "All fields are required.");
                    return View(); // Return the view with the error message displayed
                }

                // Fetch the employee details based on the 'mrcode'
                var employee = _locationContext.TblUsers
                    .Where(e => e.EmpEmail == mrcode || e.EmpName == mrcode)
                    .Select(e => new { EmpId = e.UserId, EmpName = e.EmpName, EmpEmail = e.EmpEmail })
                    .FirstOrDefault();

                // Handle the case where no employee is found
                if (employee == null)
                {
                    _logger.LogWarning("Employee not found for mrcode: {Mrcode}", mrcode);
                    ModelState.AddModelError(string.Empty, "Employee not found.");
                    return View(); // Return the view with the error message
                }

                // Parse Rscode
                var rscodeParts = selectedRscode.Split(" - ");
                string rscode = string.Empty;

                //if (rscodeParts.Length < 2)
                //{
                //    _logger.LogWarning("Invalid RSCODE format: {SelectedRscode}", selectedRscode);
                //    ModelState.AddModelError(string.Empty, "Invalid RSCODE format.");
                //    return View(); // Return the view with the error message
                //}

                rscode = rscodeParts[0];
                HttpContext.Session.SetString("RSCode", rscode);

                // Store session data
                HttpContext.Session.SetString("EmpNo", employee.EmpId.ToString());
                HttpContext.Session.SetString("SelectedOutlet", selectedOutlet);

                // Redirect to the next step
                return RedirectToAction("Create", new { progress = 40 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during AddbeforeReview.");
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred. Please try again later." });
            }
        }





        public async Task<IActionResult> Create(int progress)
        {
            try
            {
                // Retrieve session variables
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var outlet = HttpContext.Session.GetString("SelectedOutlet");

                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(outlet))
                {
                    _logger.LogWarning("Session data missing: RSCode or Outlet is empty.");
                    return RedirectToAction("Index");
                }

                // Log session data for traceability
                _logger.LogInformation("Fetching questions | RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

                // Check if there are any relevant questions before fetching them all
                bool hasQuestions = await _locationContext.QuestionsNews
                    .AsNoTracking()
                    .AnyAsync(q => q.Distributor == RSCode && q.PartyHllcode == outlet && !q.Ba && q.Status);

                if (!hasQuestions)
                {
                    _logger.LogInformation("No questions found for RSCode: {RSCode}, Outlet: {Outlet}", RSCode, outlet);
                    ViewBag.Message = "No questions found for the selected criteria.";
                    ViewBag.SysUsers = new List<QuestionsNew>();  // Return an empty list
                }
                else
                {
                    // Fetch only if questions exist
                    var questions = await _locationContext.QuestionsNews
                        .AsNoTracking()
                        .Where(q => q.Distributor == RSCode && q.PartyHllcode == outlet && !q.Ba && q.Status)
                        .ToListAsync();
                    ViewBag.SysUsers = questions;
                }

                // Retrieve saved answers from session
                var savedAnswersJson = HttpContext.Session.GetString("PrevisitAnswer");
                if (!string.IsNullOrEmpty(savedAnswersJson))
                {
                    var deserializedAnswers = JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(savedAnswersJson);
                    ViewBag.SavedAnswers = deserializedAnswers;
                }

                // Pass data to the view
                ViewBag.CurrentStep = progress;
                ViewBag.Progress = progress;

                return PartialView("~/Views/ReviewPlane/PartialViews/_Create.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching questions for RSCode: {RSCode}, Outlet: {Outlet}");
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while loading the questions. Please try again later." });
            }
        }


        [HttpPost("your-form-submit-endpoint")]
        public async Task<IActionResult> SubmitForm(IFormFile photo, [FromForm] string answers)
        {
            // Handle the answers (which is a JSON string) by deserializing it
            var answersList = JsonConvert.DeserializeObject<List<ReviewPlanAnswer>>(answers);

            // Process each answer (you can validate, save, etc.)
            foreach (var answer in answersList)
            {
                // Save answers to database or do some processing
            }

            // Handle the file (photo)
            //if (photo != null && photo.Length > 0)
            //{
            //    // Save the photo to a file path or database
            //    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", photo.FileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await photo.CopyToAsync(stream);
            //    }
            //}

            return Ok("Form submitted successfully!");
        }


        [HttpPost]
        public IActionResult Create([FromBody] ReviewPlanRequest model)
        {
            try
            {
                // Retrieve session variables
                //var RSCode = HttpContext.Session.GetString("RSCode");
                //var EmpNo = HttpContext.Session.GetString("EmpNo");
                //var outlet = HttpContext.Session.GetString("SelectedOutlet");

                //// Validate session data
                //if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
                //{
                //    _logger.LogWarning("Session data is missing.");
                //    return RedirectToAction("Index");
                //}

                //// Process file uploads and save file paths or base64 encoded data in session
                //foreach (var answer in model.Answers)
                //{
                //    if (answer.PhotoPath != null)
                //    {
                //        // Convert the image to base64 string (optional)
                //        using (var memoryStream = new MemoryStream())
                //        {
                //            await answer.PhotoPath.CopyToAsync(memoryStream);
                //            answer.Photo = Convert.ToBase64String(memoryStream.ToArray());
                //        }
                //    }
                //    else
                //    {
                //        answer.Photo = string.Empty; // Handle case where no photo is uploaded
                //    }
                //}

                //// Save only serializable data in session
                            //HttpContext.Session.SetString("PrevisitAnswer", JsonConvert.SerializeObject(model.Answers));

                return RedirectToAction("AfterCreate");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the form submission.");
                return RedirectToAction("Error", "Home");
            }
        }



        public async Task<IActionResult> AfterCreate(int progress)
        {

            var RSCode = HttpContext.Session.GetString("RSCode");
            var EmpNo = HttpContext.Session.GetString("EmpNo");
            var outlet = HttpContext.Session.GetString("SelectedOutlet");
            try
            {
                // Retrieve session variables
            

                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(outlet))
                {
                    _logger.LogWarning("Missing session data: RSCode or outlet is null or empty.");
                    return RedirectToAction("Index");
                }

                _logger.LogInformation("Fetching questions for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

                // Fetch questions based on RSCode and outlet
                var questions = await _locationContext.QuestionsNews
                    .Include(q => q.QuestionOptions)  // Ensure options are loaded
                    .Where(q => q.Distributor == RSCode && q.PartyHllcode == outlet && q.Ba == true && q.Status == true)
                    .ToListAsync();

                if (!questions.Any())
                {
                    _logger.LogInformation("No questions found for RSCode: {RSCode}, Outlet: {Outlet}", RSCode, outlet);
                    ViewBag.Message = "No questions found for the selected criteria.";
                }

                // Retrieve answers from session using the new helper method
                //var answers = GetAnswersFromSession();

                // Passing data to the view
                ViewBag.SysUsers = questions;
                ViewBag.Progress = progress;

                // Return the partial view with the data
                return PartialView("~/Views/ReviewPlane/PartialViews/_AfterCreate.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching questions for RSCode: {RSCode} and Outlet: {Outlet}", RSCode, outlet);
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while loading the questions. Please try again later." });
            }
        }



        //private List<PreviousReviewAnswerViewModel> GetAnswersFromSession()
        //{
        //    var answersJson = HttpContext.Session.GetString("PrevisitAnswer");
        //    return string.IsNullOrEmpty(answersJson)
        //        ? new List<PreviousReviewAnswerViewModel>()
        //        : JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(answersJson);
        //}




        [HttpPost]
        public async Task<IActionResult> AfterCreate(ReviewViewModel model)
        {
            try
            {
                // Retrieve session variables
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var outlet = HttpContext.Session.GetString("SelectedOutlet");



                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
                {
                    _logger.LogWarning("Session data is missing.");
                    return RedirectToAction("Index");
                }

                // Process file uploads
                foreach (var answer in model.Answers)
                {
                    if (answer.PhotoPath != null)
                    {
                        // Convert the image to base64 string (optional)
                        using (var memoryStream = new MemoryStream())
                        {
                            await answer.PhotoPath.CopyToAsync(memoryStream);
                            answer.Photo = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                    else
                    {
                        answer.Photo = string.Empty; // Handle case where no photo is uploaded
                    }
                }


                //var Previstanswer = GetAnswersFromSession();


                // Save answers in session to maintain state
                HttpContext.Session.SetString("postanswers", JsonConvert.SerializeObject(model.Answers));

                //var Previstanswer1 = GetAnswersFromSession();

                //HttpContext.Session.SetString("Previstanswer2", JsonConvert.SerializeObject(Previstanswer1));



                return RedirectToAction("Confirmation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the form submission for RSCode: {RSCode}, EmpNo: {EmpNo}");
                ModelState.AddModelError("", "An error occurred while processing your answers: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        //public async Task<IActionResult> Confirmation()
        //{
        //    try
        //    {
        //        // Retrieve session data
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");
        //        var userName = HttpContext.Session.GetString("UserName");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(SelectedOutlet))
        //        {
        //            _logger.LogWarning("Session data missing.");
        //            return RedirectToAction("Index");
        //        }

        //        var answersJson = HttpContext.Session.GetString("answers");

        //        // Deserialize both answers
        //        var answersDTO = GetAnswersFromSession();
        //        var answers = JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(answersJson);

        //        // Store the answers in the database
        //        foreach (var answer in answersDTO.Concat(answers))
        //        {
        //            if (!string.IsNullOrEmpty(answer.Photo))
        //            {
        //                byte[] photoData = Convert.FromBase64String(answer.Photo);

        //                var ReviewAnswer = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = photoData,  // Save the byte array in the database
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = SelectedOutlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(ReviewAnswer);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                // If no photo is uploaded, save the answer without a photo
        //                var answerEntity = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = null,  // No photo data to store
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = SelectedOutlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(answerEntity);
        //                await _locationContext.SaveChangesAsync();
        //            }

        //        }

        //        // Preserve 'UserName' before clearing the session
        //        HttpContext.Session.SetString("UserName", userName);

        //        // Clear the rest of the session data
        //        HttpContext.Session.Clear();

        //        // Return the confirmation partial view
        //        return PartialView("~/Views/ReviewPlane/PartialViews/_Confirmation.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error during confirmation process");
        //        return RedirectToAction("Error", "Home", new { message = "An error occurred while processing your answers." });
        //    }
        //}



        public async Task<IActionResult> Confirmation()
        {
            try
            {
                // Retrieve session data
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");

                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(SelectedOutlet))
                {
                    _logger.LogWarning("Session data missing.");
                    return RedirectToAction("Index");
                }

                var answersJson = HttpContext.Session.GetString("postanswers");

                var answersDTO = HttpContext.Session.GetString("PrevisitAnswer");


                // Deserialize both answers
                //var answersDTO = GetAnswersFromSession();
                var answers = JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(answersJson);

                var answers1 = JsonConvert.DeserializeObject<List<PreviousReviewAnswerViewModel>>(answersDTO);


                // Store the answers in the database
                foreach (var answer in answers1.Concat(answers))
                {
                    var reviewAnswer = new TblReviewAnswer
                    {
                        QuestionId = answer.QuestionId,
                        Type = answer.Type,
                        Answer = answer.Answer,
                        PhotoData = !string.IsNullOrEmpty(answer.Photo) ? Convert.FromBase64String(answer.Photo) : null,
                        EmpNo = EmpNo,
                        Rscode = RSCode,
                        Outlet = SelectedOutlet,
                        CreatedAt = DateTime.Now,
                    };

                    _locationContext.TblReviewAnswers.Add(reviewAnswer);
                    await _locationContext.SaveChangesAsync();
                }

                // Preserve 'UserName' before clearing the session

                HttpContext.Session.SetString("PrevisitAnswer", "");


                HttpContext.Session.Remove("postanswers");

                HttpContext.Session.Remove("PrevisitAnswer");



                var answersJson2 = HttpContext.Session.GetString("PrevisitAnswer");



                // Return the confirmation partial view
                return PartialView("~/Views/ReviewPlane/PartialViews/_Confirmation.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during confirmation process");
                return RedirectToAction("Error", "Home", new { message = "An error occurred while processing your answers." });
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> CreateReview([FromForm] ReviewSubmitModel model)
        //{
        //    if (model == null)
        //    {
        //        return BadRequest("Invalid data.");
        //    }

        //    if (string.IsNullOrEmpty(model.jsonData) && model.MRCode == null && model.SelectedOutlet == null && model.RSCODE == null)
        //        return BadRequest("Invalid data received.");

        //    var deserializedModel = JsonConvert.DeserializeObject<ReviewSubmitModel>(model.jsonData);
        //    var reviewAnswers = new List<TblReviewAnswer>();

        //    foreach (var answer in deserializedModel.PreAnswers.Concat(deserializedModel.PostAnswers))
        //    {
        //        if (answer.PhotoPath != null) // If the photo is uploaded as a file
        //        {
        //            try
        //            {
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await answer.PhotoPath.CopyToAsync(memoryStream);  // Copy the file to a memory stream
        //                    byte[] photoData = memoryStream.ToArray();  // Convert the memory stream to byte array

        //                    var reviewAnswer = new TblReviewAnswer
        //                    {
        //                        QuestionId = answer.QuestionId,
        //                        Type = answer.Type,
        //                        Answer = answer.Answer,
        //                        PhotoData = photoData,  // Save the byte array in the database
        //                        EmpNo = model.MRCode,
        //                        Rscode = model.RSCODE,
        //                        Outlet = model.SelectedOutlet,
        //                        CreatedAt = DateTime.Now,
        //                    };

        //                    reviewAnswers.Add(reviewAnswer);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                return BadRequest($"Error uploading photo: {ex.Message}");
        //            }
        //        }
        //        else
        //        {
        //            // If no photo is uploaded, save the answer without a photo
        //            var answerEntity = new TblReviewAnswer
        //            {
        //                QuestionId = answer.QuestionId,
        //                Type = answer.Type,
        //                Answer = answer.Answer,
        //                PhotoData = null,  // No photo data to store
        //                EmpNo = model.MRCode,
        //                Rscode = model.RSCODE,
        //                Outlet = model.SelectedOutlet,
        //                CreatedAt = DateTime.Now,
        //            };

        //            reviewAnswers.Add(answerEntity);
        //        }
        //    }

        //    // Add all answers to the database in one call
        //    _locationContext.TblReviewAnswers.AddRange(reviewAnswers);
        //    await _locationContext.SaveChangesAsync();

        //    return Ok("Review submitted successfully.");
        //}

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] ReviewSubmitModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.jsonData))
                return BadRequest("Invalid data received.");

            var deserializedModel = JsonConvert.DeserializeObject<ReviewDataModel>(model.jsonData);
            var reviewAnswers = new List<TblReviewAnswer>();

            foreach (var answer in deserializedModel.PreAnswers.Concat(deserializedModel.PostAnswers))
            {
                byte[] photoData = null;

                if (!string.IsNullOrEmpty(answer.PhotoPath))
                {
                    var uploadedFile = model.Files?.FirstOrDefault(f => f.FileName == answer.PhotoPath);
                    if (uploadedFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await uploadedFile.CopyToAsync(memoryStream);
                            photoData = memoryStream.ToArray(); // Convert to byte array
                        }
                    }
                }

                var reviewAnswer = new TblReviewAnswer
                {
                    QuestionId = answer.QuestionId,
                    Type = answer.Type,
                    Answer = answer.Answer,
                    PhotoData = photoData,  // Save as byte array
                    EmpNo = model.MRCode,
                    Rscode = model.RSCODE,
                    Outlet = model.SelectedOutlet,
                    CreatedAt = DateTime.Now,
                };

                reviewAnswers.Add(reviewAnswer);
            }

            _locationContext.TblReviewAnswers.AddRange(reviewAnswers);
            await _locationContext.SaveChangesAsync();

            return Ok("Review submitted successfully.");
        }


        public class ReviewSubmitModel
        {
            [FromForm]
            public string jsonData { get; set; }  // Holds the JSON data of answers

            [FromForm]
            public string MRCode { get; set; }  // Employee Code

            [FromForm]
            public string SelectedOutlet { get; set; }  // Selected Outlet

            [FromForm]
            public string RSCODE { get; set; }  // Some code related to the outlet

            [FromForm]
            public List<IFormFile> Files { get; set; }  // Holds the uploaded files
        }

        public class ReviewAnswerModel
        {
            public int QuestionId { get; set; }
            public string Type { get; set; }  // "text", "photo", etc.
            public string Answer { get; set; }
            public string PhotoPath { get; set; }  // Holds filename to match with uploaded files
        }

        public class ReviewDataModel
        {
            public List<ReviewAnswerModel> PreAnswers { get; set; }
            public List<ReviewAnswerModel> PostAnswers { get; set; }
        }


    



        // POST: Submit the answers from the review form


        //[HttpPost]
        //public async Task<IActionResult> Create(ReviewViewModel model)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log session data for traceability
        //        _logger.LogInformation("Processing answers for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Check if answers are provided
        //        if (model.Answers == null || !model.Answers.Any())
        //        {
        //            ModelState.AddModelError("", "No answers submitted.");
        //            _logger.LogWarning("No answers submitted for RSCode: {RSCode}, EmpNo: {EmpNo}", RSCode, EmpNo);
        //            return View(model); // Return the view with an error message
        //        }

        //        //// Process each answer
        //        //foreach (var answer in model.Answers)
        //        //{
        //        //    // Set session-based properties
        //        //    answer.Rscode = RSCode;
        //        //    answer.EmpNo = EmpNo;
        //        //    answer.Outlet = outlet;
        //        //    answer.CreatedAt = DateTime.Now; // Set the timestamp

        //        //    // Handle photo upload
        //        //    if (answer.PhotoData != null && answer.PhotoData.Length > 0)
        //        //    {
        //        //        using (var memoryStream = new MemoryStream())
        //        //        {
        //        //            answer.PhotoData = memoryStream.ToArray();  // Convert the uploaded file to byte array
        //        //        }
        //        //    }

        //        //    // Add the answer to the DbContext for insertion
        //        //    _locationContext.ReviewAnswers.Add(answer);
        //        //}

        //        // Save all answers to the database
        //        await _locationContext.SaveChangesAsync();

        //        // Log successful submission
        //        _logger.LogInformation("Successfully saved {AnswerCount} answers for RSCode: {RSCode}, EmpNo: {EmpNo}", model.Answers.Count, RSCode, EmpNo);

        //        // Calculate and store the progress
        //        int progress = model.Answers.Count * 2; // Assuming 2% for each answer
        //        if (progress > 100) progress = 100;

        //        // Store progress in session
        //        HttpContext.Session.SetInt32("progress", progress);

        //        // Return a confirmation view or redirect
        //        ViewBag.Progress = progress;
        //        return RedirectToAction("ReviewPhoto", new { progress = progress });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while saving answers for RSCode: {RSCode}, EmpNo: {EmpNo}",
        //                         HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

        //        // Add a generic error message
        //        ModelState.AddModelError("", "An error occurred while saving your answers: " + ex.Message);

        //        // Redirect to error page with the exception message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while saving your answers. Please try again later." });
        //    }
        //}





        //[HttpPost]
        //public async Task<IActionResult> Create(ReviewViewModel model)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log session data for traceability
        //        _logger.LogInformation("Processing answers for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Validate answers list
        //        if (model.Answers == null || !model.Answers.Any())
        //        {
        //            ModelState.AddModelError("", "No answers submitted.");
        //            _logger.LogWarning("No answers submitted for RSCode: {RSCode}, EmpNo: {EmpNo}", RSCode, EmpNo);
        //            return View(); // Return the view with an error message
        //        }

        //        foreach (var answer in model.Answers)
        //        {
        //            byte[] photoData = null;

        //            if (answer.PhotoPath != null && answer.PhotoPath.Length > 0)
        //            {
        //                // Process the photo (if it exists)
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await answer.PhotoPath.CopyToAsync(memoryStream);
        //                    photoData = memoryStream.ToArray();
        //                }

        //                var ReviewAnswer = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = photoData,  // Save the byte array in the database
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(ReviewAnswer);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                // If no photo is uploaded, save the answer without a photo
        //                var answerEntity = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = null,  // No photo data to store
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(answerEntity);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //        }

        //        // Retrieve progress value from session
        //        int progress = 0;
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert byte array to int
        //        }

        //        // Check if each field is selected and update the progress accordingly
        //        if (!string.IsNullOrEmpty(RSCode)) progress += 2;  // Add 20% for RSCODE
        //        if (!string.IsNullOrEmpty(EmpNo)) progress += 2;    // Add 20% for MR Code
        //        if (!string.IsNullOrEmpty(outlet)) progress += 2;   // Add 20% for Outlet
        //        if (model.Answers.Any()) progress += model.Answers.Count * 2; // Add 5% for each answer

        //        if (progress > 100) progress = 100;

        //        // Update session with new progress value
        //        HttpContext.Session.SetInt32("progress", progress);

        //        _logger.LogInformation("Successfully saved {AnswerCount} answers for RSCode: {RSCode}, EmpNo: {EmpNo}", model.Answers.Count, RSCode, EmpNo);

        //        // Redirect to the confirmation or success page
        //        ViewBag.Progress = progress;

        //        return RedirectToAction("AfterCreate", new { progress = progress });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while saving answers for RSCode: {RSCode}, EmpNo: {EmpNo}",
        //                         HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

        //        // Add a generic error message
        //        ModelState.AddModelError("", "An error occurred while saving your answers: " + ex.Message);

        //        // Redirect to error page with the exception message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while saving your answers. Please try again later." });
        //    }
        //}









        //[HttpPost]
        //public async Task<IActionResult> AfterCreate(ReviewViewModel model)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log session data for traceability
        //        _logger.LogInformation("Processing answers for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Validate answers list
        //        if (model.Answers == null || !model.Answers.Any())
        //        {
        //            ModelState.AddModelError("", "No answers submitted.");
        //            _logger.LogWarning("No answers submitted for RSCode: {RSCode}, EmpNo: {EmpNo}", RSCode, EmpNo);
        //            return View(); // Return the view with an error message
        //        }

        //        foreach (var answer in model.Answers)
        //        {
        //            byte[] photoData = null;

        //            if (answer.PhotoPath != null && answer.PhotoPath.Length > 0)
        //            {
        //                // Process the photo (if it exists)
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    await answer.PhotoPath.CopyToAsync(memoryStream);
        //                    photoData = memoryStream.ToArray();
        //                }

        //                var ReviewAnswer = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = photoData,  // Save the byte array in the database
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(ReviewAnswer);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //            else
        //            {
        //                // If no photo is uploaded, save the answer without a photo
        //                var answerEntity = new TblReviewAnswer
        //                {
        //                    QuestionId = answer.QuestionId,
        //                    Type = answer.Type,
        //                    Answer = answer.Answer,
        //                    PhotoData = null,  // No photo data to store
        //                    EmpNo = EmpNo,
        //                    Rscode = RSCode,
        //                    Outlet = outlet,
        //                    CreatedAt = DateTime.Now,
        //                };

        //                _locationContext.TblReviewAnswers.Add(answerEntity);
        //                await _locationContext.SaveChangesAsync();
        //            }
        //        }

        //        // Retrieve progress value from session
        //        int progress = 0;
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert byte array to int
        //        }

        //        // Check if each field is selected and update the progress accordingly
        //        if (!string.IsNullOrEmpty(RSCode)) progress += 2;  // Add 20% for RSCODE
        //        if (!string.IsNullOrEmpty(EmpNo)) progress += 2;    // Add 20% for MR Code
        //        if (!string.IsNullOrEmpty(outlet)) progress += 2;   // Add 20% for Outlet
        //        if (model.Answers.Any()) progress += model.Answers.Count * 2; // Add 5% for each answer

        //        if (progress > 100) progress = 100;

        //        // Update session with new progress value
        //        HttpContext.Session.SetInt32("progress", progress);

        //        _logger.LogInformation("Successfully saved {AnswerCount} answers for RSCode: {RSCode}, EmpNo: {EmpNo}", model.Answers.Count, RSCode, EmpNo);

        //        // Redirect to the confirmation or success page
        //        ViewBag.Progress = progress;

        //        return RedirectToAction("Confirmation", new { progress = progress });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while saving answers for RSCode: {RSCode}, EmpNo: {EmpNo}",
        //                         HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

        //        // Add a generic error message
        //        ModelState.AddModelError("", "An error occurred while saving your answers: " + ex.Message);

        //        // Redirect to error page with the exception message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while saving your answers. Please try again later." });
        //    }
        //}



























        //[HttpPost]
        //public async Task<IActionResult> AfterCreate(List<ReviewAnswer> answers)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log session data for traceability
        //        _logger.LogInformation("Processing answers for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Validate answers list
        //        if (answers == null || !answers.Any())
        //        {
        //            ModelState.AddModelError("", "No answers submitted.");
        //            _logger.LogWarning("No answers submitted for RSCode: {RSCode}, EmpNo: {EmpNo}", RSCode, EmpNo);
        //            return View(); // Return the view with an error message
        //        }


        //        int progress = 0;
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert the byte array back to an int
        //        }


        //        if (!string.IsNullOrEmpty(RSCode)) progress += 10;  // Add 20% for RSCODE
        //        if (!string.IsNullOrEmpty(EmpNo)) progress += 10;        // Add 20% for MR Code
        //        if (!string.IsNullOrEmpty(outlet)) progress += 20;  // Add 20% for Outlet
        //                                                            // You can add more checks for other fields if necessary

        //        if (answers.Any()) progress += answers.Count * 2;  // Add 5% for each answer (you can adjust as needed)
        //        if (progress > 100) progress = 100;

        //        // Process each answer
        //        foreach (var answer in answers)
        //        {


        //            // Set session-based properties
        //            answer.Rscode = RSCode;
        //            answer.EmpNo = EmpNo;
        //            answer.Outlet = outlet;
        //            answer.CreatedAt = DateTime.Now; // Set the timestamp

        //            // Add the answer to the DbContext for insertion
        //            _locationContext.ReviewAnswers.Add(answer);
        //        }

        //        // Save all answers to the database
        //        await _locationContext.SaveChangesAsync();

        //        // Log successful submission
        //        _logger.LogInformation("Successfully saved {AnswerCount} answers for RSCode: {RSCode}, EmpNo: {EmpNo}", answers.Count, RSCode, EmpNo);

        //        ViewBag.Progress = progress;

        //        // Redirect to the confirmation or success page
        //        return RedirectToAction("AfterReviewPhoto", new { progress = progress });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred while saving answers for RSCode: {RSCode}, EmpNo: {EmpNo}",
        //                         HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

        //        // Add a generic error message
        //        ModelState.AddModelError("", "An error occurred while saving your answers: " + ex.Message);

        //        // Redirect to error page with the exception message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while saving your answers. Please try again later." });
        //    }
        //}

        public IActionResult AfterReviewPhoto(int progress)
        {
            try
            {
                // Retrieve session variables
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var outlet = HttpContext.Session.GetString("SelectedOutlet");

                HttpContext.Session.Set("progress", BitConverter.GetBytes(progress)); // Store progress as byte[]


                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
                {
                    _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
                    return RedirectToAction("Index"); // Redirect to Index if session data is missing
                }

                // Log session data for traceability
                _logger.LogInformation("Reviewing photo for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

                // Here, you can add logic to retrieve photos or any other necessary data for the review
                // For example, fetching photo review data based on RSCode, EmpNo, and Outlet

                // Example: var photos = _locationContext.Photos.Where(p => p.Rscode == RSCode && p.Outlet == outlet).ToList();

                // Return the PartialView

                if (progress > 100) progress = 100;


                ViewBag.RSCode = RSCode;
                ViewBag.EmpNo = EmpNo;
                ViewBag.SelectedOutlet = outlet;

                ViewBag.progress = progress;


                return PartialView("~/Views/ReviewPlane/PartialViews/_AfterReviewPhoto.cshtml");
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while reviewing photo for RSCode: {RSCode}, EmpNo: {EmpNo}",
                                 HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

                // Redirect to error page with the exception message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while reviewing the photo. Please try again later." });
            }
        }



        [HttpPost]
        public async Task<IActionResult> AfterReviewPhoto(
        IFormFile PhotoUpload1,
        IFormFile PhotoUpload2,
        IFormFile PhotoUpload3,
        string Question1,
        string Question2,
        string Question3)
        {
            try
            {
                // Validate session data
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");

                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(SelectedOutlet))
                {
                    _logger.LogWarning("Missing session data: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, SelectedOutlet);
                    return RedirectToAction("Index"); // Redirect if session data is missing
                }

                //Validate that at least one question is answered
                if (string.IsNullOrEmpty(Question1) && string.IsNullOrEmpty(Question2) && string.IsNullOrEmpty(Question3))
                {
                    ModelState.AddModelError("", "At least one question must be answered.");
                    return RedirectToAction("Error", "Home", new { message = "At least one question must be answered." });
                }

                // Check if the files are uploaded and handle them as needed
                string photoPath1 = null;
                string photoPath2 = null;
                string photoPath3 = null;


                int progress = 0;


                if (HttpContext.Session.TryGetValue("progress", out var progressValue))
                {
                    progress = BitConverter.ToInt32(progressValue, 0); // Convert the byte array back to an int
                }

                // If PhotoUpload1 is provided
                if (PhotoUpload1 != null && PhotoUpload1.Length > 0)
                {
                    photoPath1 = PhotoUpload1.FileName; // Store only the file name, or generate a unique name if needed
                    progress += 5; // Update progress to 20% after the first photo upload

                }

                // If PhotoUpload2 is provided
                if (PhotoUpload2 != null && PhotoUpload2.Length > 0)
                {
                    photoPath2 = PhotoUpload2.FileName; // Store only the file name, or generate a unique name if needed
                    progress += 5; // Update progress to 20% after the first photo upload

                }


                if (progress == 0)
                {
                    ModelState.AddModelError("", "At least one photo must be uploaded.");
                    return RedirectToAction("Error", "Home", new { message = "At least one photo must be uploaded." });
                }

                // Set the progress in the session
                HttpContext.Session.Set("progress", BitConverter.GetBytes(progress)); // Save progress to session



                // Create the review entry in the database
                var review = new ReviewsPhoto
                {
                    Rscode = RSCode,
                    EmpNo = EmpNo,
                    SelectedOutlet = SelectedOutlet,
                    Question1 = Question1,
                    Question2 = Question2,
                    Question3 = Question3,
                    PhotoPath1 = photoPath1,
                    PhotoPath2 = photoPath2,
                    PhotoPath3 = photoPath3
                };

                // Add the review to the database
                _locationContext.ReviewsPhotos.Add(review);
                await _locationContext.SaveChangesAsync();

                // Log success message
                _logger.LogInformation("Review submitted successfully for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, SelectedOutlet);

                // Set a success message to be shown after submission
                TempData["SuccessMessage"] = "Review submitted successfully!";

                // Redirect to the confirmation page
                return RedirectToAction("Confirmation");
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while submitting the review for RSCode: {RSCode}, EmpNo: {EmpNo}",
                                 HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

                // Redirect to error page with the exception message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while submitting the review. Please try again later." });
            }
        }



        public IActionResult ReviewPhoto(int progress)
        {
            try
            {
                // Retrieve session variables
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var outlet = HttpContext.Session.GetString("SelectedOutlet");

                HttpContext.Session.Set("progress", BitConverter.GetBytes(progress)); // Store progress as byte[]


                // Validate session data
                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(outlet))
                {
                    _logger.LogWarning("Session data is missing: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);
                    return RedirectToAction("Index"); // Redirect to Index if session data is missing
                }

                // Log session data for traceability
                _logger.LogInformation("Reviewing photo for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

                // Here, you can add logic to retrieve photos or any other necessary data for the review
                // For example, fetching photo review data based on RSCode, EmpNo, and Outlet

                // Example: var photos = _locationContext.Photos.Where(p => p.Rscode == RSCode && p.Outlet == outlet).ToList();

                // Return the PartialView





                ViewBag.RSCode = RSCode;
                ViewBag.EmpNo = EmpNo;
                ViewBag.SelectedOutlet = outlet;
                ViewBag.progress = progress;



                return PartialView("~/Views/ReviewPlane/PartialViews/_ReviewPhoto.cshtml");
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while reviewing photo for RSCode: {RSCode}, EmpNo: {EmpNo}",
                                 HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

                // Redirect to error page with the exception message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while reviewing the photo. Please try again later." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ReviewPhoto(
    IFormFile PhotoUpload1,
    IFormFile PhotoUpload2,

    string Question1,
    string Question2,
    string Question3)
        {
            try
            {
                // Validate session data
                var RSCode = HttpContext.Session.GetString("RSCode");
                var EmpNo = HttpContext.Session.GetString("EmpNo");
                var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");



                if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(SelectedOutlet))
                {
                    _logger.LogWarning("Missing session data: RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, SelectedOutlet);
                    return RedirectToAction("Index"); // Redirect if session data is missing
                }

                // Validate that at least one question is answered
                if (string.IsNullOrEmpty(Question1) && string.IsNullOrEmpty(Question2) && string.IsNullOrEmpty(Question3))
                {
                    ModelState.AddModelError("", "At least one question must be answered.");
                    return RedirectToAction("Error", "Home", new { message = "At least one question must be answered." });
                }

                // Check if the files are uploaded and handle them as needed
                string photoPath1 = null;
                string photoPath2 = null;
                string photoPath3 = null;

                // Initial progress
                int progress = 0;


                if (HttpContext.Session.TryGetValue("progress", out var progressValue))
                {
                    progress = BitConverter.ToInt32(progressValue, 0); // Convert the byte array back to an int
                }

                // If PhotoUpload1 is provided, progress is updated
                if (PhotoUpload1 != null && PhotoUpload1.Length > 0)
                {
                    photoPath1 = PhotoUpload1.FileName; // Store only the file name, or generate a unique name if needed
                    progress += 5; // Update progress to 20% after the first photo upload
                }

                // If PhotoUpload2 is provided, progress is updated
                if (PhotoUpload2 != null && PhotoUpload2.Length > 0)
                {
                    photoPath2 = PhotoUpload2.FileName; // Store only the file name, or generate a unique name if needed
                    progress += 5; // Update progress to 40% after the second photo upload
                }



                // Validate that at least one photo is uploaded to complete the process
                if (progress == 0)
                {
                    ModelState.AddModelError("", "At least one photo must be uploaded.");
                    return RedirectToAction("Error", "Home", new { message = "At least one photo must be uploaded." });
                }

                // Set the progress in the session
                HttpContext.Session.Set("progress", BitConverter.GetBytes(progress)); // Save progress to session

                // Create the review entry in the database
                var review = new ReviewsPhoto
                {
                    Rscode = RSCode,
                    EmpNo = EmpNo,
                    SelectedOutlet = SelectedOutlet,
                    Question1 = Question1,
                    Question2 = Question2,
                    Question3 = Question3,
                    PhotoPath1 = photoPath1,
                    PhotoPath2 = photoPath2,
                    PhotoPath3 = photoPath3
                };

                // Add the review to the database
                _locationContext.ReviewsPhotos.Add(review);
                await _locationContext.SaveChangesAsync();

                // Log success message
                _logger.LogInformation("Review submitted successfully for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, SelectedOutlet);

                // Set a success message to be shown after submission
                TempData["SuccessMessage"] = "Review submitted successfully!";

                // Redirect to the confirmation page
                return RedirectToAction("AfterCreate", new { progress = progress });
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred while submitting the review for RSCode: {RSCode}, EmpNo: {EmpNo}",
                                 HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("EmpNo"));

                // Redirect to error page with the exception message
                return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while submitting the review. Please try again later." });
            }
        }




        // GET: Confirmation Page after submitting answers
        //public IActionResult Confirmation()
        //{
        //    try
        //    {
        //        // Check if session data exists before removing
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var SelectedOutlet = HttpContext.Session.GetString("SelectedOutlet");

        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(EmpNo) || string.IsNullOrEmpty(SelectedOutlet))
        //        {
        //            _logger.LogWarning("Session data missing: RSCode: {RSCode}, EmpNo: {EmpNo}, SelectedOutlet: {SelectedOutlet}", RSCode, EmpNo, SelectedOutlet);
        //            return RedirectToAction("Index"); // Redirect if session data is missing
        //        }

        //        // Clear session data after the review process
        //        HttpContext.Session.Remove("SelectedOutlet");
        //        HttpContext.Session.Remove("RSCode");
        //        HttpContext.Session.Remove("EmpNo");

        //        // Log successful review submission
        //        _logger.LogInformation("Review submission completed for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {SelectedOutlet}", RSCode, EmpNo, SelectedOutlet);

        //        // Pass a success message to the view
        //        ViewBag.Message = "Your review has been submitted successfully!";

        //        // Return the confirmation partial view
        //        return PartialView("~/Views/ReviewPlane/PartialViews/_Confirmation.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details
        //        _logger.LogError(ex, "An error occurred during the confirmation process.");

        //        // Redirect to error page with the exception message
        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred during the confirmation process. Please try again later." });
        //    }
        //}





        [HttpGet]
        public IActionResult GetRscodeOptions(string query)
        {
            var rsCodes = _locationContext.TblDistributors
                .Where(d => d.DistributorName.Contains(query) || d.Distributor.ToString().Contains(query)) // Convert 'Distributor' to string for comparison
                .Select(d => new { d.DistributorName, d.Distributor })
                .Distinct()
                .ToList();

            return Json(rsCodes);
        }




        [HttpGet]
        public IActionResult GetEmpSuggestions(string query)
        {
            var empSuggestions = _locationContext.TblUsers
                .Where(e => e.EmpName.Contains(query) || e.EmpNo.Contains(query)) // Search by EmpName or EmpNo
                .Select(e => new { e.EmpName, e.EmpNo,e.UserId })
                .Distinct()
                .ToList();

            return Json(empSuggestions);
        }





        public JsonResult GetEmployees()
        {
            var employees = _locationContext.TblUsers
                                    .Select(u => new { u.EmpName, u.EmpNo, u.IsActive })
                                    .ToList();

            return Json(employees);
        }






        public IActionResult FilteredReviewAnswers()
        {




            var model = new FilteredReviewAnswersViewModel();



            return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", model);
        }




        //[HttpPost]
        //public IActionResult FilteredReviewAnswers(string RSCode, string EmpName, int? year, int? month, DateTime? date)
        //{
        //    // Initialize the query for TblReviewAnswers
        //    var query = _locationContext.TblReviewAnswers.AsQueryable();

        //    // Validate RSCode exists in TblDistributors
        //    string distributorCode = null;
        //    if (!string.IsNullOrEmpty(RSCode))
        //    {
        //        var distributor = _locationContext.TblDistributors
        //            .FirstOrDefault(d => d.Distributor.ToString() == RSCode); // Correct comparison of RSCode with Distributor
        //        if (distributor != null)
        //        {
        //            distributorCode = distributor.Distributor.ToString(); // Store the valid RSCode
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("RSCode", "Invalid RSCode.");
        //        }
        //    }

        //    // Validate EmpName exists in TblUser (mapping EmpName to EmpNo)
        //    int? userNo = null;
        //    if (!string.IsNullOrEmpty(EmpName))
        //    {
        //        var user = _locationContext.TblUsers
        //.FirstOrDefault(u => u.EmpName.ToLower() == EmpName.ToLower()); // Case-insensitive comparison using ToLower()

        //        if (user != null)
        //        {
        //            userNo = user.UserId; // Store the valid UserId
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("EmpName", "Invalid EmpName.");
        //        }
        //    }

        //    // Apply filters based on RSCode and EmpNo if available
        //    if (!string.IsNullOrEmpty(distributorCode))
        //    {
        //        query = query.Where(e => e.Rscode == distributorCode); // Filter by valid RSCode
        //    }

        //    if (userNo.HasValue)
        //    {
        //        query = query.Where(e => e.EmpNo == userNo.ToString()); // Filter by valid EmpNo (UserId)
        //    }

        //    // Apply additional filters for year, month, and date
        //    if (year.HasValue)
        //    {
        //        query = query.Where(x => x.CreatedAt.Year == year.Value);
        //    }
        //    if (month.HasValue)
        //    {
        //        query = query.Where(x => x.CreatedAt.Month == month.Value);
        //    }
        //    if (date.HasValue)
        //    {
        //        query = query.Where(x => x.CreatedAt.Date == date.Value.Date);
        //    }

        //    // Check if the query has results
        //    var result = query.ToList();

        //    // If no results, return an empty list
        //    if (!result.Any())
        //    {
        //        return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", new List<TblReviewAnswer>());
        //    }

        //    // Return the filtered results to the partial view
        //    return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", result);
        //}



        //[HttpPost]
        //public IActionResult FilteredReviewAnswers(string RSCode, string EmpName)
        //{
        //    // Initialize the query for TblReviewAnswers
        //    var query = _locationContext.TblReviewAnswers.AsQueryable();

        //    // Validate and filter by RSCode if provided
        //    if (!string.IsNullOrEmpty(RSCode))
        //    {
        //        var distributor = _locationContext.TblDistributors
        //            .FirstOrDefault(d => d.Distributor.ToString() == RSCode);

        //        if (distributor != null)
        //        {
        //            query = query.Where(e => e.Rscode == distributor.Distributor.ToString());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("RSCode", "Invalid RSCode.");
        //        }
        //    }

        //    // Validate and filter by EmpName if provided
        //    if (!string.IsNullOrEmpty(EmpName))
        //    {
        //        var user = _locationContext.TblUsers
        //            .FirstOrDefault(u => u.EmpName.ToLower() == EmpName.ToLower());

        //        if (user != null)
        //        {
        //            query = query.Where(e => e.EmpNo == user.UserId.ToString());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("EmpName", "Invalid EmpName.");
        //        }
        //    }

        //    // Execute the query and get the result
        //    var result = query.ToList();

        //    // If no results found, return an empty list
        //    if (result == null || !result.Any())
        //    {
        //        return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", new List<TblReviewAnswer>());
        //    }

        //    // Return the filtered results to the view
        //    return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", result);
        //}


        //[HttpPost]
        //public IActionResult FilteredReviewAnswers(string RSCode, string EmpName, string sortBy, int page = 1, int pageSize = 10)
        //{
        //    IQueryable<TblReviewAnswer> query = _locationContext.TblReviewAnswers.AsQueryable();

        //    // Apply filtering by RSCode
        //    if (!string.IsNullOrEmpty(RSCode))
        //    {
        //        var distributor = _locationContext.TblDistributors
        //            .FirstOrDefault(d => d.Distributor.ToString() == RSCode);

        //        if (distributor != null)
        //        {
        //            query = query.Where(e => e.Rscode == distributor.Distributor.ToString());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("RSCode", "Invalid or missing RSCode.");
        //        }
        //    }

        //    // Apply filtering by EmpName
        //    if (!string.IsNullOrEmpty(EmpName))
        //    {
        //        var user = _locationContext.TblUsers
        //            .FirstOrDefault(u => u.EmpName.ToLower() == EmpName.ToLower());

        //        if (user != null)
        //        {
        //            query = query.Where(e => e.EmpNo == user.UserId.ToString());
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("EmpName", "Invalid or missing EmpName.");
        //        }
        //    }

        //    // Apply sorting (if provided)
        //    if (!string.IsNullOrEmpty(sortBy))
        //    {
        //        if (sortBy == "CreatedAt")
        //        {
        //            query = query.OrderBy(e => e.CreatedAt);
        //        }
        //        else if (sortBy == "EmpNo")
        //        {
        //            query = query.OrderBy(e => e.EmpNo);
        //        }
        //    }

        //    // Apply pagination
        //    query = query.Skip((page - 1) * pageSize).Take(pageSize);

        //    // Map to the view model
        //    var result = query.Select(e => new ReviewAnswerViewModel
        //    {
        //        Rscode = e.Rscode,
        //        EmpNo = e.EmpNo,

        //        CreatedAt = e.CreatedAt,
        //        PhotoBase64 = e.PhotoData != null ? Convert.ToBase64String(e.PhotoData) : null

        //    }).ToList();

        //    // Handle empty result
        //    if (result == null || !result.Any())
        //    {
        //        return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", new List<ReviewAnswerViewModel>());
        //    }

        //    return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", result);
        //}












        [HttpPost]
        public async Task<IActionResult> FilteredReviewAnswers(string rscode, string? EmpName, DateTime? startDate, DateTime? endDate)
        {
            var query = _locationContext.TblReviewAnswers.AsQueryable();
            var model = new FilteredReviewAnswersViewModel();

            // Apply filters based on input
            if (!string.IsNullOrEmpty(rscode))
                query = query.Where(r => r.Rscode == rscode);

            string[] parts = EmpName.Split(',');
            string UserId = parts[1].Trim();  // e.g., "8"


            // Filter by EmpName if provided
            if (!string.IsNullOrEmpty(EmpName))
            {

                if (parts.Length > 1)
                {

                    if (!string.IsNullOrEmpty(UserId))
                    {
                        query = query.Where(r => r.EmpNo == UserId);
                    }
                    else
                    {
                        model.ErrorMessage = "Employee not found.";
                        return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", model);
                    }
                }
                else
                {
                    model.ErrorMessage = "Invalid Employee name format.";
                    return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", model);
                }
            }

            // Check if startDate is before endDate
            if (startDate.HasValue && endDate.HasValue)
            {
                if (startDate.Value > endDate.Value)
                {
                    model.ErrorMessage = "Start date cannot be later than end date. Please check the dates.";
                    return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", model);
                }
                else
                {
                    query = query.Where(r => r.CreatedAt >= startDate && r.CreatedAt < endDate.Value.AddDays(1));
                }
            }

            //// Fetch the data with a left join to handle missing data (DefaultIfEmpty)
            //var results = await (from qn in _locationContext.QuestionsNews
            //                     join tra in _locationContext.TblReviewAnswers
            //                     on qn.Distributor equals tra.Rscode into groupJoin
            //                     from tra in groupJoin.DefaultIfEmpty()  // Use DefaultIfEmpty to handle missing data
            //                     where tra.CreatedAt.Date >= startDate && tra.CreatedAt.Date <= endDate
            //                           && tra.Rscode == rscode && tra.EmpNo == UserId
            //                           && tra.QuestionId == qn.QuestionId && tra.Outlet == qn.PartyHllcode
            //                     orderby tra.CreatedAt descending  // Sort by CreatedAt date and time (latest first)
            //                     select new
            //                     {
            //                         qn.Question,
            //                         qn.Distributor,
            //                         qn.PartyHllcode,
            //                         qn.PartyMasterCode,
            //                         Answer = (tra != null && tra.Answer != null) ? tra.Answer : "",  // Safely handle NULL values
            //                         PhotoData = (tra != null && tra.PhotoData != null) ? Convert.ToBase64String(tra.PhotoData) : ""  // Safely handle NULL values
            //                     }).ToListAsync(); // Get all records matching the criteria

            //// Now perform grouping and projection in memory
            //var groupedResults = results
            //    .GroupBy(r => r.Question)  // Group by Question
            //    .Select(group => new
            //    {
            //        Question = group.Key,
            //        Reviews = group.ToList()  // Get all items in the group
            //    })
            //    .ToList();


            //// Perform the client-side projection after grouping
            //var viewModelResults = groupedResults.SelectMany(group => group.Reviews, (group, item) => new ReviewAnswerViewModel
            //{
            //    Question = item.Question,
            //    Distributor = item.Distributor,
            //    PartyHllcode = item.PartyHllcode,
            //    PartyMasterCode = item.PartyMasterCode,
            //    CombinedData = (item.Answer != null || item.PhotoData != null)
            //        ? (item.Answer ?? "") + " " + (item.PhotoData != "" ? item.PhotoData : "")
            //        : "Not Answer"
            //}).ToList();


            var rankedAnswers = (from qn in _locationContext.QuestionsNews
                                 join tra in _locationContext.TblReviewAnswers
                                 on qn.QuestionId equals tra.QuestionId
                                 where tra.CreatedAt.Date >= startDate && tra.CreatedAt.Date <= endDate
                                       && tra.Rscode == rscode
                                 select new
                                 {
                                     qn.Question,
                                     qn.Distributor,
                                     qn.PartyHllcode,
                                     qn.PartyMasterCode,
                                     CombinedData = (tra.Answer == null && tra.PhotoData == null)
                                                    ? "Not Answer"
                                                    : (tra.Answer ?? "") + " " + (tra.PhotoData != null ? Convert.ToBase64String(tra.PhotoData) : ""),
                                     tra.CreatedAt,
                                     qn.QuestionId
                                 })
                    .AsEnumerable() // To handle in-memory operations
                    .GroupBy(a => new { a.QuestionId, CreatedAt = a.CreatedAt.Date }) // Group by QuestionId and date
                    .Select(group => group.OrderByDescending(x => x.CreatedAt) // Order by CreatedAt descending
                        .FirstOrDefault()) // Take the first record for each group
                    .ToList(); // Execute the query and materialize the results

            // Now, you can map it to your view model
            var viewModelResults = rankedAnswers.Select(item => new ReviewAnswerViewModel
            {
                Question = item.Question,
                Distributor = item.Distributor,
                PartyHllcode = item.PartyHllcode,
                PartyMasterCode = item.PartyMasterCode,
                CombinedData = item.CombinedData,
               // CreatedAt = item.CreatedAt
            }).ToList();




            model.Results = viewModelResults;

            // Return the view with results
            return PartialView("~/Views/ReviewPlane/PartialViews/_FilteredReviewAnswers.cshtml", model);
        }











        //public async Task<IActionResult> PreviousCreate(int progress)
        //{
        //    try
        //    {
        //        // Retrieve session variables
        //        var RSCode = HttpContext.Session.GetString("RSCode");
        //        var EmpNo = HttpContext.Session.GetString("EmpNo");
        //        var outlet = HttpContext.Session.GetString("SelectedOutlet");

        //        // Retrieve the progress value from the session
        //        if (HttpContext.Session.TryGetValue("progress", out var progressValue))
        //        {
        //            progress = BitConverter.ToInt32(progressValue, 0); // Convert the byte array back to an int
        //        }

        //        // Validate session data
        //        if (string.IsNullOrEmpty(RSCode) || string.IsNullOrEmpty(outlet))
        //        {
        //            _logger.LogWarning("Missing session data: RSCode or outlet is null or empty.");
        //            return RedirectToAction("Index"); // Redirect to Index if session data is missing
        //        }

        //        // Log the session data for traceability
        //        _logger.LogInformation("Fetching questions for RSCode: {RSCode}, EmpNo: {EmpNo}, Outlet: {Outlet}", RSCode, EmpNo, outlet);

        //        // Fetch questions based on RSCode and outlet
        //        var questions = await _locationContext.QuestionsNews
        //            .Include(q => q.QuestionOptions) // Ensure options are loaded
        //            .Where(q => q.Distributor == RSCode && q.PartyHllcode == outlet && q.Ba == false && q.Status == true)
        //            .ToListAsync();

        //        // Fetch previously saved answers
        //        var existingAnswers = await _locationContext.TblReviewAnswers
        //            .Where(a => a.Rscode == RSCode && a.EmpNo == EmpNo && a.Outlet == outlet)
        //            .ToListAsync();

        //        // Map existing answers to questions
        //        var viewModel = questions.Select(q => new PreviousReviewAnswerViewModel
        //        {
        //            QuestionId = q.QuestionId,
        //            Type = q.Type,
        //            QuestionText = q.Question,
        //            Answer = existingAnswers.FirstOrDefault(a => a.QuestionId == q.QuestionId)?.Answer,
        //            PhotoData = existingAnswers.FirstOrDefault(a => a.QuestionId == q.QuestionId)?.PhotoData
        //        }).ToList();

        //        // If no questions were found, log the information
        //        if (!questions.Any())
        //        {
        //            _logger.LogInformation("No questions found for RSCode: {RSCode}, Outlet: {Outlet}", RSCode, outlet);
        //            ViewBag.Message = "No questions found for the selected criteria.";
        //        }

        //        ViewBag.CurrentStep = progress;
        //        ViewBag.SysUsers = viewModel;
        //        ViewBag.Progress = progress; // Pass the progress value to the view

        //        return PartialView("~/Views/ReviewPlane/PartialViews/_Create.cshtml", viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while fetching questions for RSCode: {RSCode} and Outlet: {Outlet}",
        //            HttpContext.Session.GetString("RSCode"), HttpContext.Session.GetString("SelectedOutlet"));

        //        return RedirectToAction("Error", "Home", new { message = "An unexpected error occurred while loading the questions. Please try again later." });
        //    }
        //}



        [HttpPost]
        public IActionResult SaveReviewData(List<PreviousReviewAnswerViewModel> answers)
        {
            // Save the answers to session
            HttpContext.Session.SetString("ReviewAnswers", JsonConvert.SerializeObject(answers));

            return Ok();  // Return success response
        }













       





    }
}
