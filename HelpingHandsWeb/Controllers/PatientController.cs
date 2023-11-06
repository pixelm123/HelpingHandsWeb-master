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
            var viewModel = new PatientIndexViewModel(userDisplayName, _configuration);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                viewModel.TotalCareVisits = connection.QueryFirstOrDefault<int>("GetTotalCareVisits", commandType: CommandType.StoredProcedure);
                viewModel.TotalCareContracts = connection.QueryFirstOrDefault<int>("GetTotalChronicConditions", commandType: CommandType.StoredProcedure);

                // Call the new method to get patient conditions
                viewModel.PatientConditions = GetPatientConditions(viewModel.PatientId);

                var nurseResults = connection.Query<PatientAppointmentsViewModel>("GetPatientsAppointments", commandType: CommandType.StoredProcedure);
                viewModel.Appointments = nurseResults.ToList();
            }

            ViewData["UserName"] = userDisplayName;

            return View("PatientDashboard", viewModel);
        }

        [HttpPost("PatientDashboard")]
        public IActionResult PatientDashboard(PatientIndexViewModel model)
        {
            return View("PatientDashboard", model);
        }

        // New method to get patient conditions
        // New method to get patient conditions
        private List<PatientConditionsViewModel> GetPatientConditions(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var results = connection.Query<PatientConditionsViewModel>(
                    "GetPatientCondition",
                    new { PatientID = patientId },
                    commandType: CommandType.StoredProcedure
                );

                return results.ToList(); // Explicitly convert to List<PatientConditionsViewModel>
            }
        }

        [HttpPost("change-password")]
        public IActionResult cc(PatientIndexViewModel model)
        {
            return View("change-password", model);
        }

        [HttpPost("profile")]
        public IActionResult Profile(PatientProfileViewModel model)
        {
            // Implementation
            return View("profile", model);
        }

    }
}











//using Microsoft.AspNetCore.Mvc;
//using Dapper;
//using Microsoft.Extensions.Configuration;
//using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
//using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
//using HelpingHandsWeb.Models.ViewModels.PatientViewModels;

//using System.Data;
//using System.Data.SqlClient;
//using System;
//using HelpingHandsWeb.Data;

//namespace HelpingHandsWeb.Controllers
//{
//    public class PatientController : BaseController
//    {
//        public PatientController(ApplicationDbContext context, IConfiguration configuration)
//            : base(context, configuration)
//        {
//        }

//        [HttpGet("PatientDashboard")]
//        public IActionResult PatientDashboard()
//        {
//            var userDisplayName = GetUserDisplayName();
//            var viewModel = new PatientIndexViewModel(userDisplayName, _configuration);

//            using (SqlConnection connection = new SqlConnection(ConnectionString))
//            {
//                connection.Open();

//                viewModel.TotalPatientCareVisits = connection.QueryFirstOrDefault<int>("GetPatientTotalCareVisits", commandType: CommandType.StoredProcedure);
//                viewModel.TotalPatientCareContracts = connection.QueryFirstOrDefault<int>("GetPatientTotalChronicConditions", commandType: CommandType.StoredProcedure);

//                var patientResults = connection.Query<PatientConditionsViewModel>("GetPatientsConditions", commandType: CommandType.StoredProcedure);
//                viewModel.PatientConditions = patientResults.ToList();

//                var nurseResults = connection.Query<PatientAppointmentsViewModel>("GetPatientsAppointment", commandType: CommandType.StoredProcedure);
//                viewModel.PatientAppointments = nurseResults.ToList();
//            }

//            ViewData["UserDisplayName"] = userDisplayName;
//            return View("PatientDashboard", viewModel);
//        }

//        [HttpPost("AdminDashboard")]
//        public IActionResult AdminDashboard(AdminIndexViewModel model)
//        {
//            return View("AdminDashboard", model);
//        }

//        [HttpGet("Patients")]
//        public IActionResult Patients()
//        {
//            using (SqlConnection connection = new SqlConnection(ConnectionString))
//            {
//                connection.Open();
//                var patients = connection.Query<PatientViewModel>("GetPatients", commandType: CommandType.StoredProcedure);
//                return View("~/Views/Admin/patients.cshtml", patients);
//            }
//        }

//        [HttpPost("EditPatient")]
//        public IActionResult EditPatient(PatientViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                using (SqlConnection connection = new SqlConnection(ConnectionString))
//                {
//                    connection.Open();
//                    connection.Execute("UpdatePatient", new
//                    {
//                        UserName = model.UserName,
//                        Password = model.Password,
//                        Email = model.Email,
//                        ContactNo = model.ContactNo,
//                        UserType = "P",
//                        Status = "A",
//                        ProfilePicture = (object)DBNull.Value,
//                        FirstName = model.FirstName,
//                        Surname = model.Surname,
//                        Gender = model.Gender,
//                        DateOfBirth = model.DateOfBirth,
//                        EmergencyPerson = model.EmergencyPerson,
//                        EmergencyContactNo = model.EmergencyContactNo
//                    }, commandType: CommandType.StoredProcedure);
//                }
//                return RedirectToAction("Patients");
//            }
//            return View("~/Views/Admin/edit-patient.cshtml", model);
//        }

//        [HttpGet("DeletePatient/{userId}")]
//        public IActionResult DeletePatient(int userId)
//        {
//            using (SqlConnection connection = new SqlConnection(ConnectionString))
//            {
//                connection.Open();
//                connection.Execute("DeletePatient", new { UserID = userId }, commandType: CommandType.StoredProcedure);
//            }
//            return RedirectToAction("Patients");
//        }
//    }
//}
