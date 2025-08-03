using BLL.Services;
using DAL.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model_New.Models;

namespace MR_Application_New.Controllers
{
    public class LoginController : Controller
    {
        private readonly IGenericRepository<TblUser, MrAppDbNewContext> _loginService;

        private readonly ILogger<LoginController> _logger;


        private CommonService commonService = new CommonService();



        private readonly IGenericRepository<TblSystemUser, MrAppDbNewContext> generic;
        //public LoginController(IGenericRepository<TblUser,MrAppDbNewContext> loginService, ILogger<LoginController> logger)
        //{
        //    _loginService = loginService;
        //    _logger = logger;  // Initialize logger
        //}

        public LoginController(IGenericRepository<TblUser, MrAppDbNewContext> loginService, IGenericRepository<TblSystemUser, MrAppDbNewContext> _generic, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            generic = _generic;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Login/Index")]
        public IActionResult Index(string Email, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    _logger.LogError("Email or Password is null or empty.");
                    return RedirectToAction("Error", "Home", new { message = "Email and password cannot be null or empty." });
                }

                _logger.LogInformation("Login attempt for email: {Email}", Email);

                // Get the user from CommonService
                var user = commonService.GetUsers(Email, Password);

                if (user == null || !user.Any())
                {
                    _logger.LogWarning("Invalid login attempt for email: {Email}", Email);
                    return RedirectToAction("Denied", "Home");
                }

                var currentUser = user.FirstOrDefault();
                if (currentUser == null || currentUser.IsActive == null || currentUser.IsActive == false)
                {
                    _logger.LogWarning("Inactive or non-existent user attempted login for email: {Email}", Email);
                    return RedirectToAction("Error", "Home", new { message = "This account is inactive. Please contact support." });
                }

                // Set session variables
                HttpContext.Session.SetString("UserName", currentUser.EmpName ?? ""); // Ensure EmpName is not null
                HttpContext.Session.SetString("UserEmail", currentUser.EmpEmail ?? "");
                HttpContext.Session.SetString("UserNo", currentUser.EmpNo ?? "");

                if (!string.IsNullOrEmpty(currentUser.EmpNo))
                {
                    var sysUsers = commonService.GetSystemUser(currentUser.EmpNo, (bool)currentUser.IsActive);
                    var currentSysUser = sysUsers?.FirstOrDefault();

                    if (currentSysUser != null)
                    {
                        var userType = (currentSysUser.UserTypeId == 2) ? "Admin" : "NormalUser";

                        _logger.LogInformation("{UserType} login successful for email: {Email}", userType, Email);

                        // Redirect based on user type
                        return userType == "Admin"
                            ? RedirectToAction("Index", "Admin")
                            : RedirectToAction("Welcome"); // Redirect to normal user welcome action

                    }
                    else
                    {
                        _logger.LogWarning("System user not found for EmpNo: {EmpNo}", currentUser.EmpNo);
                        return RedirectToAction("Error", "Home", new { message = "User type information could not be retrieved." });
                    }
                }

                _logger.LogWarning("User EmpNo is null or empty for email: {Email}", Email);
                return RedirectToAction("Error", "Home", new { message = "Invalid user data." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login.");
                return RedirectToAction("Error", new { message = "An unexpected error occurred." });
            }
        }





        //public List<TblUser> GetUsers(string Email, string Password) {

        //    var users = .TblUser
        //   .Where(user => user.Email == Email && user.Password == Password)
        //   .ToList();
        //    return  users;
        //}







        [Route("Login/Welcome")]
        public IActionResult Welcome()
        {

            ViewBag.IsUsers = User.IsInRole("Users");

            _logger.LogInformation("Welcome page accessed");  // Log access to the Welcome page
            return View();
        }


        //[Route("Login/Welcome")]
        //public IActionResult Welcome()
        //{
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
        //    {
        //        _logger.LogWarning("Session expired, redirecting to login.");
        //        return RedirectToAction("Index"); // Redirect to login if session has expired
        //    }

        //    ViewBag.UserName = HttpContext.Session.GetString("UserName");
        //    ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
        //    ViewBag.UserNo = HttpContext.Session.GetString("UserNo");

        //    _logger.LogInformation("Welcome page accessed");  // Log access to the Welcome page
        //    return View();
        //}



        //[Route("Login/Admin")]
        //public IActionResult Admin()
        //{

        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
        //    {
        //        _logger.LogWarning("Session expired, redirecting to login.");
        //        return RedirectToAction("Index"); // Redirect to login if session has expired
        //    }

        //    ViewBag.IsAdmin = User.IsInRole("Admin");


        //    _logger.LogInformation("AdminDashboard page accessed");  // Log access to the Welcome page
        //    return View();
        //}



        [HttpPost]
        [Route("Login/Logout")]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Login"); // Redirect to login page
        }


    }
}
