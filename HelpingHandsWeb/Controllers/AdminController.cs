using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HelpingHandsWeb.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserDisplayName()
        {
            return HttpContext.Session.GetString("UserDisplayName");
        }

        [HttpGet("AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            var userDisplayName = GetUserDisplayName();

            var viewModel = new AdminIndexViewModel(userDisplayName);

            ViewData["UserDisplayName"] = userDisplayName;

            return View("~/Views/Admin/AdminDashboard.cshtml", viewModel);
        }

        [HttpPost("AdminDashboard")]
        public IActionResult AdminDashboard(AdminIndexViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/AdminDashboard.cshtml", model);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(AdminIndexViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/change-password.cshtml", model);
        }

        [HttpPost("profile")]
        public IActionResult Profile(ProfileViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/profile.cshtml", model);
        }

        // City Actions
        [HttpPost("add-city")]
        public IActionResult AddCity(CityViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/add-city.cshtml", model);
        }

        [HttpPost("edit-city")]
        public IActionResult EditCity(CityViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/edit-city.cshtml", model);
        }

        [HttpPost("cities")]
        public IActionResult Cities(CityViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/cities.cshtml", model);
        }

        // Condition Actions
        [HttpPost("add-condition")]
        public IActionResult AddCondition(ConditionViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/add-condition.cshtml", model);
        }

        [HttpPost("edit-condition")]
        public IActionResult EditCondition(ConditionViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/edit-condition.cshtml", model);
        }

        [HttpPost("conditions")]
        public IActionResult Conditions(ConditionViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/conditions.cshtml", model);
        }

        // Nurse Actions
        [HttpPost("add-nurse")]
        public IActionResult AddNurse(NurseViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/add-nurse.cshtml", model);
        }

        [HttpPost("edit-nurse")]
        public IActionResult EditNurse(NurseViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/edit-nurse.cshtml", model);
        }

        [HttpPost("nurses")]
        public IActionResult Nurses(NurseViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/nurses.cshtml", model);
        }

        // OfficeManager Actions
        [HttpPost("add-officemanager")]
        public IActionResult AddOfficeManager(OfficeManagerViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/add-officemanager.cshtml", model);
        }

        [HttpPost("edit-officemanager")]
        public IActionResult EditOfficeManager(OfficeManagerViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/edit-officemanager.cshtml", model);
        }

        [HttpPost("officemanagers")]
        public IActionResult OfficeManagers(OfficeManagerViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/officemanagers.cshtml", model);
        }

        // Other Entity Actions
        [HttpPost("patients")]
        public IActionResult Patients(PatientViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/patients.cshtml", model);
        }

        [HttpPost("suburbs")]
        public IActionResult Suburbs(SuburbViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/suburbs.cshtml", model);
        }
    }
}
