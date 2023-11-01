using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace HelpingHandsWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        private string GetUserType(string userName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<string>("GetLoginUserType", new { UserName = userName },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                string userType = GetUserType(model.UserName);

                if (userType == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }

                // Set UserType in ViewBag
                ViewBag.UserType = userType;

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var parameters = new
                    {
                        UserName = model.UserName,
                        Password = model.Password
                    };

                    int authenticationResult = connection.QueryFirstOrDefault<int>("LoginAuthenticateUser", parameters,
                        commandType: CommandType.StoredProcedure);

                    if (authenticationResult == 1)
                    {
                        HttpContext.Session.SetString("IsAuthenticated", "true");

                        return RedirectToDashboard(userType);
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password.");
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred during login.");
                return View(model);
            }
        }

        private IActionResult RedirectToDashboard(string userType)
        {
            switch (userType)
            {
                case "A":
                    Console.WriteLine("Redirecting to Admin Dashboard");
                    return RedirectToAction("AdminDashboard", "Admin");

                case "O":
                    Console.WriteLine("Redirecting to Office Manager Dashboard");
                    return RedirectToAction("OfficeManagerDashboard", "OfficeManager");

                case "N":
                    Console.WriteLine("Redirecting to Nurse Dashboard");
                    return RedirectToAction("NurseDashboard", "Nurse");

                case "P":
                    Console.WriteLine("Redirecting to Patient Dashboard");
                    return RedirectToAction("PatientDashboard", "Patient");

                default:
                    Console.WriteLine($"Invalid user type: {userType}");
                    return RedirectToAction("Error", "Home");
            }
        }
    }
}
