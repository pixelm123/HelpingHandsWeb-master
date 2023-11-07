using HelpingHandsWeb.Data;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using HelpingHandsWeb.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Models.Users;
using System.Collections.Generic;

namespace HelpingHandsWeb.Controllers
{
    public class PatientController : BaseController
    {
        public PatientController(ApplicationDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        [HttpGet("PatientDashboard")]
        public IActionResult PatientDashboard()
        {
            var userDisplayName = GetUserDisplayName();
            var viewModel = new PatientIndexViewModel(userDisplayName, _configuration, this);

          
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                viewModel.TotalCareVisits = GetTotalCareContracts(userId);
                viewModel.TotalCareContracts = GetTotalCareVisits(userId);


               
                viewModel.PatientConditions = GetPatientConditions(userId).ToList();

                viewModel.Appointments = GetPatientAppointments(userId);
            }

            ViewData["UserName"] = userDisplayName;
            return View("PatientDashboard", viewModel);
        }

        [HttpPost("PatientDashboard")]
        public IActionResult PatientDashboard(PatientIndexViewModel model)
        {
            return View("PatientDashboard", model);
        }

        // Inside PatientController
        [HttpPost("change-password")]
        public IActionResult ChangePassword(PatientProfileViewModel model)
        {
            var userName = GetUserDisplayName();
            var userId = GetUserId(userName);

            // Assuming you have fields in the model for old, new, and confirm passwords
            // Replace these with your actual property names
            var oldPassword = model.OldPassword;
            var newPassword = model.NewPassword;
            var confirmPassword = model.ConfirmPassword;

            // Validate the old password (add your own logic here)
            if (!ValidateOldPassword(userId, oldPassword))
            {
                // Handle invalid old password
                ModelState.AddModelError("OldPassword", "Invalid old password");
                return View("change-password", model);
            }

            // Validate new and confirm passwords
            if (newPassword != confirmPassword)
            {
                // Handle password mismatch
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View("change-password", model);
            }

            // Update the password
            UpdatePassword(userId, newPassword);

            // Redirect or show success message
            return RedirectToAction("PatientProfile");
        }

        // Inside PatientController
        private bool ValidateOldPassword(int userId, string oldPassword)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("oldPassword", oldPassword);

                // Fetch the user's stored password from the database
                var storedPassword = connection.QueryFirstOrDefault<string>(
                    @"SELECT Password FROM [USER] WHERE UserID = @userId;",
                    parameters
                );

                // Compare the provided old password with the stored password
                return storedPassword == oldPassword;
            }
        }





        [HttpGet("profile")]
        public IActionResult PatientProfile()
        {
            var userName = GetUserDisplayName();
            var userId = GetUserId(userName);

            // Fetch data from USER table
            var user = GetUserById(userId);

            // Fetch data from PATIENTS table
            var patient = GetPatientById(userId);

            // Fetch the patient's chronic conditions
            var chronicConditions = GetPatientConditions(userId);

            var profileViewModel = new PatientProfileViewModel
            {
                // Populate user and patient information
                UserName = userName,
                FirstName = patient.FirstName,
                Surname = patient.Surname,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
                EmergencyPerson = patient.EmergencyPerson,
                EmergencyContactNo = patient.EmergencyContactNo,
                // Add other properties as needed
                ChronicConditions = chronicConditions
            };

            return View("profile", profileViewModel);
        }


        [HttpPost("profile")]
        public IActionResult EditProfile(PatientProfileViewModel model)
        {
            // Implementation
            return View("edit-profile", model);
        }



    }
}




