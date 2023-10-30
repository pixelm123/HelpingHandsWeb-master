using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace HelpingHandsWeb.Controllers
{
    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string ConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("DefaultConnection");
            }
        }

        [HttpGet("Patients")]
        public IActionResult Patients()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                
                var patients = connection.Query<PatientViewModel>("GetPatients", commandType: CommandType.StoredProcedure);

                return View("~/Views/Admin/patients.cshtml", patients);
            }
        }

        [HttpPost("EditPatient")]
        public IActionResult EditPatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                
                    connection.Execute("UpdatePatient", new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "P",
                        Status = "A",
                        ProfilePicture = (object)DBNull.Value,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        DateOfBirth = model.DateOfBirth,
                        EmergencyPerson = model.EmergencyPerson,
                        EmergencyContactNo = model.EmergencyContactNo
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Patients");
            }
            return View("~/Views/Admin/edit-patient.cshtml", model);
        }

        [HttpGet("DeletePatient/{userId}")]
        public IActionResult DeletePatient(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                
                connection.Execute("DeletePatient", new { UserID = userId }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Patients");
        }
    }
}
