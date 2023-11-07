using HelpingHandsWeb.Data;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models;
using HelpingHandsWeb.Models.Users;
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
using HelpingHandsWeb.Helper;

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


        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(PatientProfileViewModel model)
        {
            var userName = GetUserDisplayName();
            var userId = GetUserId(userName);

            var oldPassword = model.OldPassword;
            var newPassword = model.NewPassword;
            var confirmPassword = model.ConfirmPassword;


            if (!ValidateOldPassword(userId, oldPassword))
            {

                ModelState.AddModelError("OldPassword", "Invalid old password");
                return View("change-password", model);
            }


            if (newPassword != confirmPassword)
            {

                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View("change-password", model);
            }


            UpdatePassword(userId, newPassword);


            return RedirectToAction("PatientProfile");
        }


        private bool ValidateOldPassword(int userId, string oldPassword)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("oldPassword", oldPassword);


                var storedPassword = connection.QueryFirstOrDefault<string>(
                    @"SELECT Password FROM [USER] WHERE UserID = @userId;",
                    parameters
                );

                return storedPassword == oldPassword;
            }
        }

        [HttpGet("Profile")]
        public IActionResult PatientProfile()
        {
            var userName = GetUserDisplayName();
            var userId = GetUserId(userName);

            var patient = GetPatientById(userId);
            var user = GetUserById(userId);

          
            var selectedConditions = GetPatientConditions(userId);
        
            var chronicConditions = selectedConditions.Select(pc => new PatientConditionsViewModel
            {
                ConditionID = pc.ConditionID,
                Name = pc.Name,
                Description = pc.Description,
                IsDeleted = pc.IsDeleted
              
            }).ToList();


            var profileViewModel = new PatientProfileViewModel
            {
                UserName = userName,
                FirstName = patient.FirstName,
                Surname = patient.Surname,
                Gender = patient.Gender.ToString(),
                DateOfBirth = patient.DateOfBirth,
                EmergencyPerson = patient.EmergencyPerson,
                EmergencyContactNo = patient.EmergencyContactNo,
                ContactNo = user.ContactNo,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture,
                Password = user.Password,
                ChronicConditions = chronicConditions
            };

            return View("patient-profile", profileViewModel);
        }


        [HttpPost("EditProfile")]
        public IActionResult EditProfile(PatientProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = GetUserDisplayName();
                var userId = GetUserId(userName);
                var user = GetUserById(userId);
                var patient = GetPatientById(userId);
                patient.FirstName = model.FirstName;
                patient.Surname = model.Surname;

                patient.Gender = string.IsNullOrEmpty(model.Gender) ? '\0' : model.Gender[0];

                patient.DateOfBirth = model.DateOfBirth;
                patient.EmergencyPerson = model.EmergencyPerson;
                patient.EmergencyContactNo = model.EmergencyContactNo;
                user.ContactNo = model.ContactNo;
                user.Email = model.Email;
                user.ProfilePicture = model.ProfilePicture;

                _context.SaveChanges();

                UpdatePatientChronicConditions(userId, model.ChronicConditions.Select(cc => cc.ConditionID).ToList());



                return RedirectToAction("Profile");
            }

            return View("edit-profile", model);
        }

        [HttpGet("RequestCareContract")]
        public IActionResult RequestCareContract()
        {
            var suburbs = SuburbHelper.GetSuburbsFromDatabase(null, null, 0, false);
            ViewBag.Suburbs = new SelectList(suburbs, "SuburbId", "SuburbName");
            return View();
        }



        [HttpPost("RequestCareContract")]
        public IActionResult RequestCareContract(CareContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var userName = GetUserDisplayName();
                var userId = GetUserId(userName);

            
                var careContract = new CareContract
                {
                    PatientID = userId,
                    ContractDate = DateTime.Now,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    SuburbId = model.SuburbId,
                    WoundDescription = model.WoundDescription,
                    StartCareDate = null, 
                    EndCareDate = null,   
                    NurseID = null,      
                    ContractStatus = "New", 
                    IsDeleted = false
                };

                
                _context.CareContracts.Add(careContract);
                _context.SaveChanges();

                return RedirectToAction("PatientDashboard");
            }

          
            var suburbs = SuburbHelper.GetSuburbsFromDatabase(null, null, 0, false);
            ViewBag.Suburbs = new SelectList(suburbs, "SuburbId", "SuburbName");
            return View(model);
        }



    }
}




