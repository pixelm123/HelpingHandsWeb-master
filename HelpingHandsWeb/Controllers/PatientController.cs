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




