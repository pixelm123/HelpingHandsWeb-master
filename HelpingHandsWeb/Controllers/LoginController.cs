using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HelpingHandsWeb.Controllers
{
    public class LoginController : Controller
    {
        public readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel(); 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
           
                string userType = GetUserType(model.UserName);

                if (userType == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }

                ViewBag.UserType = userType;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("LoginAuthenticateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", model.UserName);
                        command.Parameters.AddWithValue("@Password", model.Password);



                        int authenticationResult = (int)command.ExecuteScalar();

                        if (authenticationResult == 1)
                        {
                            HttpContext.Session.SetString("IsAuthenticated", "true");

                            if (userType == "A")
                            {
                                Console.WriteLine("Redirecting to Admin Dashboard");
                                return RedirectToAction("AdminDashboard", "Admin");

                            }
                            else if (userType == "O")
                            {
                                Console.WriteLine("Redirecting to Office Manager Dashboard");
                                return RedirectToAction("OfficeManagerDashboard", "OfficeManager");
                            }
                            else if (userType == "N")
                            {
                                Console.WriteLine("Redirecting to Nurse Dashboard");
                                return RedirectToAction("NurseDashboard", "Nurse");
                            }
                            else if (userType == "P")
                            {
                                return RedirectToAction("PatientDashboard", "Patient");
                            }
                            else
                            {
                                //ModelState.AddModelError(string.Empty, "Invalid user type.");
                                //return View(model);

                                return RedirectToAction("AdminDashboard", "Admin");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                            ModelState.AddModelError(string.Empty, "Invalid username or password.");
                            return View(model);

                        }
                    }
                }
            

            //return View(model);
        }

        private string GetUserType(string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetLoginUserType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);

                    return (string)command.ExecuteScalar();
                }
            }
        }
    }
}