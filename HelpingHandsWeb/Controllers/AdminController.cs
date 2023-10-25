using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


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

        [HttpGet("cities")]
        public IActionResult Cities()
        {
            var cities = _context.Cities
                .Where(c => !c.IsDeleted)
                .Select(c => new CityViewModel
                {
                    CityId = c.CityId,
                    Name = c.Name,
                    Abbreviation = c.Abbreviation
                })
                .ToList();

            return View("~/Views/Admin/cities.cshtml", cities);
        }

        [HttpGet("edit-city/{id}")]
        public IActionResult EditCity(int id)
        {
            var city = _context.Cities
                .Where(c => c.CityId == id && !c.IsDeleted)
                .Select(c => new CityViewModel
                {
                    CityId = c.CityId,
                    Name = c.Name,
                    Abbreviation = c.Abbreviation
                })
                .FirstOrDefault();

            if (city == null)
            {
                return RedirectToAction("Cities");
            }

            return View("~/Views/Admin/edit-city.cshtml", city);
        }

        [HttpPost("edit-city")]
        public IActionResult EditCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = _context.Cities
                    .Where(c => c.CityId == model.CityId && !c.IsDeleted)
                    .FirstOrDefault();

                if (city != null)
                {
                    city.Name = model.Name;
                    city.Abbreviation = model.Abbreviation;

                    _context.SaveChanges();
                }

                return RedirectToAction("Cities");
            }

            return View("~/Views/Admin/edit-city.cshtml", model);
        }

        [HttpPost("delete-city/{id}")]
        public IActionResult DeleteCity(int id)
        {
            var city = _context.Cities
                .Where(c => c.CityId == id && !c.IsDeleted)
                .FirstOrDefault();

            if (city != null)
            {
                city.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Cities");
        }

        [HttpGet("add-city")]
        public IActionResult AddCity()
        {
            return View("~/Views/Admin/add-city.cshtml", new CityViewModel());
        }

        [HttpPost("add-city")]
        public IActionResult AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = new City
                {
                    Name = model.Name,
                    Abbreviation = model.Abbreviation
                };

                _context.Cities.Add(city);
                _context.SaveChanges();

                return RedirectToAction("Cities");
            }

            return View("~/Views/Admin/add-city.cshtml", model);
        }

        [HttpGet("suburbs")]
        public IActionResult Suburbs()
        {
            var suburbs = _context.Suburbs
                .Where(s => !s.IsDeleted)
                .Select(s => new SuburbViewModel
                {
                    SuburbID = s.SuburbID,
                    SuburbName = s.SuburbName,
                    PostalCode = s.PostalCode,
                    Name = s.City.Name // Assuming you have a navigation property in Suburb class for City
                })
                .ToList();

            return View("~/Views/Admin/suburbs.cshtml", suburbs);
        }

        [HttpGet("edit-suburb/{id}")]
        public IActionResult EditSuburb(int id)
        {
            var suburb = _context.Suburbs
                .Where(s => s.SuburbID == id && !s.IsDeleted)
                .Select(s => new SuburbViewModel
                {
                    SuburbID = s.SuburbID,
                    SuburbName = s.SuburbName,
                    PostalCode = s.PostalCode,
                    CityId = s.CityId // Assuming CityId is a property in Suburb class
                })
                .FirstOrDefault();

            if (suburb == null)
            {
                return RedirectToAction("Suburbs");
            }

            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", suburb.CityId);

            return View("~/Views/Admin/edit-suburb.cshtml", suburb);
        }

        [HttpPost("edit-suburb")]
        public IActionResult EditSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                var suburb = _context.Suburbs
                    .Where(s => s.SuburbID == model.SuburbID && !s.IsDeleted)
                    .FirstOrDefault();

                if (suburb != null)
                {
                    suburb.SuburbName = model.SuburbName;
                    suburb.PostalCode = model.PostalCode;
                    suburb.CityId = model.CityId;

                    _context.SaveChanges();
                }

                return RedirectToAction("Suburbs");
            }

            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);

            return View("~/Views/Admin/edit-suburb.cshtml", model);
        }

        [HttpPost("delete-suburb/{id}")]
        public IActionResult DeleteSuburb(int id)
        {
            var suburb = _context.Suburbs
                .Where(s => s.SuburbID == id && !s.IsDeleted)
                .FirstOrDefault();

            if (suburb != null)
            {
                suburb.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Suburbs");
        }

        [HttpGet("add-suburb")]
        public IActionResult AddSuburb()
        {
            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name");

            return View("~/Views/Admin/add-suburb.cshtml", new SuburbViewModel());
        }

        [HttpPost("add-suburb")]
        public IActionResult AddSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                var suburb = new Suburb
                {
                    SuburbName = model.SuburbName,
                    PostalCode = model.PostalCode,
                    CityId = model.CityId
                };

                _context.Suburbs.Add(suburb);
                _context.SaveChanges();

                return RedirectToAction("Suburbs");
            }

            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);

            return View("~/Views/Admin/add-suburb.cshtml", model);
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
