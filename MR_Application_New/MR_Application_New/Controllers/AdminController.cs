//using BLL.Services;
//using DAL;
//using DAL.IRepositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Model_New.Models;

//namespace MR_Application_New.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly UnitOfWork<MrAppDbNewContext> unitOfWork;
//        private readonly ILogger<AdminController> _logger;

//        // Constructor with dependency injection
//        public AdminController(

//            UnitOfWork<MrAppDbNewContext> unitOfWork,
//            ILogger<AdminController> logger)
//        {

//            this.unitOfWork = unitOfWork;
//            _logger = logger;
//        }

//        // Admin Dashboard
//        [Route("Admin/Index")]
//        public IActionResult Index()
//        {
//            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
//            {
//                _logger.LogWarning("Session expired, redirecting to login.");
//                return RedirectToAction("Index"); // Redirect to login if session has expired
//            }

//            ViewBag.IsAdmin = User.IsInRole("Admin");
//            _logger.LogInformation("AdminDashboard page accessed");  // Log access to the Welcome page

//            // ViewBag.Tabs = new List<string> { "Tab1", "Tab2", "Tab3" };

//            return View();
//        }

//        // Get the form to create a distributor


//        public IActionResult DistributionsList()
//        {
//            var distributions = unitOfWork.Tbl_Distributor.GetAll().ToList();
//            return PartialView("~/Views/Admin/PartialViews/_DistributionsList.cshtml", distributions);
//        }


//        [HttpGet]
//        public IActionResult AddDistribution()
//        {
//            try
//            {
//                _logger.LogInformation("Accessed the Add Distribution form.");
//                return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml");
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error while rendering AddDistribution form.");
//                return StatusCode(500, "Internal Server Error");
//            }
//        }




//        //[HttpPost]
//        //public IActionResult AddDistribution(TblDistributor distribution)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        unitOfWork.Tbl_Distributor.AddAsync(distribution);
//        //        unitOfWork.SaveAsync();
//        //        _logger.LogInformation($"Added a new distributor: {distribution.DistributorName}");
//        //        return RedirectToAction("DistributionsList");
//        //    }

//        //    _logger.LogWarning("Invalid input for adding a distributor.");
//        //    return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml", distribution);
//        //}


//        [HttpPost]

//        [Route("/Admin/AddDistribution")]
//        public async Task<IActionResult> AddDistribution(TblDistributor distribution)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await unitOfWork.Tbl_Distributor.AddAsync(distribution);
//                    await unitOfWork.SaveAsync();  // Ensure async save
//                    _logger.LogInformation($"Added a new distributor: {distribution.DistributorName}");
//                    return RedirectToAction("index");
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex, "Error while adding the distributor.");
//                    return StatusCode(500, "Internal Server Error");
//                }
//            }

//            _logger.LogWarning("Invalid input for adding a distributor.");
//            return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml", distribution);
//        }




//    }
//}


using BLL.Services;
using DAL;
using DAL.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model_New.Models;

namespace MR_Application_New.Controllers
{
    public class AdminController : Controller
    {
        private readonly UnitOfWork<MrAppDbNewContext> unitOfWork;
        private readonly ILogger<AdminController> _logger;

        // Constructor with dependency injection
        public AdminController(
            UnitOfWork<MrAppDbNewContext> unitOfWork,
            ILogger<AdminController> logger)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
        }

        // Admin Dashboard
        [Route("Admin/Index")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                _logger.LogWarning("Session expired, redirecting to login.");
                TempData["AlertMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Index", "Login"); // Redirect to login
            }

            ViewBag.IsAdmin = User.IsInRole("Admin");
            _logger.LogInformation("AdminDashboard page accessed");

            return View();
        }

        // Distributions List
        public IActionResult DistributionsList()
        {
            try
            {
                var distributions = unitOfWork.Tbl_Distributor.GetAll().ToList();
                return PartialView("~/Views/Admin/PartialViews/_DistributionsList.cshtml", distributions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving distribution list.");
                TempData["AlertMessage"] = "An error occurred while fetching the distribution list.";
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get the form to add a distribution
        [HttpGet]
        public IActionResult AddDistribution()
        {
            try
            {
                _logger.LogInformation("Accessed the Add Distribution form.");
                return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while rendering AddDistribution form.");
                TempData["AlertMessage"] = "An error occurred while accessing the Add Distribution form.";
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Add Distribution
        //[HttpPost]
        //[Route("/Admin/AddDistribution")]
        //public async Task<IActionResult> AddDistribution(TblDistributor distribution)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Invalid input for adding a distributor.");
        //        TempData["AlertMessage"] = "Please correct the errors in the form.";
        //        return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml", distribution);
        //    }

        //    try
        //    {
        //        // Check if the distributor already exists
        //        var existingDistributor = await unitOfWork.Tbl_Distributor
        //            .FindAsync(d => d.DistributorName == distribution.DistributorName);

        //        if (existingDistributor != null)
        //        {
        //            TempData["AlertMessage"] = "A distributor with this name already exists.";
        //            return PartialView("~/Views/Admin/PartialViews/_AddDistribution.cshtml", distribution);
        //        }

        //        await unitOfWork.Tbl_Distributor.AddAsync(distribution);
        //        await unitOfWork.SaveAsync(); // Save changes asynchronously

        //        TempData["AlertMessage"] = "Distributor added successfully!";
        //        _logger.LogInformation($"Added a new distributor: {distribution.DistributorName}");

        //        return RedirectToAction("DistributionsList");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while adding the distributor.");
        //        TempData["AlertMessage"] = "An error occurred while saving the distributor. Please try again.";
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}



        [HttpPost]
        public async Task<IActionResult> AddDistribution(TblDistributor distribution)
        {
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Invalid data. Please correct the highlighted errors.";
                TempData["AlertType"] = "warning"; // Alert type for validation errors
                return Json(new { success = false, errors = ModelState.ToDictionary(k => k.Key, v => string.Join(", ", v.Value.Errors.Select(e => e.ErrorMessage))) });
            }

            try
            {
                await unitOfWork.Tbl_Distributor.AddAsync(distribution);
                await unitOfWork.SaveAsync();

                TempData["AlertMessage"] = "Distributor added successfully!";
                TempData["AlertType"] = "success"; // Alert type for success
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding distributor.");
                TempData["AlertMessage"] = "An error occurred while saving the distributor.";
                TempData["AlertType"] = "danger"; // Alert type for errors
                return Json(new { success = false });
            }
        }


        [HttpGet]
        public IActionResult ADDMRMaster()
        {
            try
            {
                _logger.LogInformation("Accessed the Add MRMaster form.");
                return PartialView("~/Views/Admin/PartialViews/_ADDMRMaster.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while rendering AddDistribution form.");
                TempData["AlertMessage"] = "An error occurred while accessing the Add Distribution form.";
                return StatusCode(500, "Internal Server Error");
            }
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddMrMaster(TblMrmaster mrMaster)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await unitOfWork.Tbl_Mrmaster.AddAsync(mrMaster);  // Assuming AddAsync method exists in the repository
        //            await unitOfWork.SaveAsync();  // Save the changes to the database
        //            _logger.LogInformation($"Added a new MrMaster: {mrMaster.BpName}");

        //            TempData["AlertMessage"] = "MrMaster added successfully!";
        //            TempData["AlertType"] = "success";
        //            return RedirectToAction("MrMasterList"); // Redirect to list or another action
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error while adding MrMaster.");
        //            TempData["AlertMessage"] = "An error occurred while adding MrMaster.";
        //            TempData["AlertType"] = "danger";
        //            return View(mrMaster); // Return to form in case of error
        //        }
        //    }

        //    // If validation fails, return to the form
        //    TempData["AlertMessage"] = "Please correct the errors in the form.";
        //    TempData["AlertType"] = "warning";
        //    return View(mrMaster);
        //}




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMrMaster(TblMrmaster mrMaster)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Validation error: {error.ErrorMessage}");
                }

                TempData["AlertMessage"] = "Please correct the errors in the form.";
                TempData["AlertType"] = "warning";
                return View(mrMaster); // Return to form with errors
            }

            // Continue with form processing if model is valid
            try
            {
                await unitOfWork.Tbl_Mrmaster.AddAsync(mrMaster);
                await unitOfWork.SaveAsync();
                TempData["AlertMessage"] = "MR Master added successfully!";
                TempData["AlertType"] = "success";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding MR Master.");
                TempData["AlertMessage"] = "An error occurred while adding MR Master.";
                TempData["AlertType"] = "danger";
                return View(mrMaster); // Return form with error
            }
        }



        public IActionResult Outletcreate()
        {
            // Create an instance of OutLetMasterDetail and pass it to the view
            var model = new OutLetMasterDetail();
            return PartialView("~/Views/Admin/PartialViews/_Outletcreate.cshtml", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Outletcreate([Bind("Id, Rscode, RsName, PartyMasterCode, PartyHllcode, PartyName, PrimaryChannel, SecondaryChannel, Category, ParStatus, UpdateStamp, OlCreatedDate, Address1, Address2, Address3, Address4, Latitude, Longitude, PrimarychannelCode, SecondarychannelCode")] OutLetMasterDetail outletMasterDetail)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.OutL_etMasterDetails.AddAsync(outletMasterDetail);
                await unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(outletMasterDetail);
        }


    }
}

