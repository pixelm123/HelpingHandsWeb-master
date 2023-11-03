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
namespace HelpingHandsWeb.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AdminController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private string GetUserDisplayName()
        {
            return HttpContext.Session.GetString("UserDisplayName");
        }
        private string ConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("DefaultConnection");
            }
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
        [HttpPost("add-city")]
        public IActionResult AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertCity", new
                    {
                        Name = model.Name,
                        Abbreviation = model.Abbreviation
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Cities");
            }
            return View("~/Views/Admin/add-city.cshtml", model);
        }

        [HttpPost("edit-city")]
        public IActionResult EditCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateCity", new
                    {
                        CityId = model.CityId,
                        Name = model.Name,
                        Abbreviation = model.Abbreviation
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Cities");
            }
            return View("~/Views/Admin/edit-city.cshtml", model);
        }

        [HttpPost("delete-city/{id}")]
        public IActionResult DeleteCity(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteCity", new
                {
                    CityId = id
                }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Cities");
        }

        [HttpGet("cities")]
        public IActionResult Cities()
        {
            var model = new List<CityViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<CityViewModel>("GetCity", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            return View("~/Views/Admin/cities.cshtml", model);
        }
        [HttpGet("add-city")]
        public IActionResult AddCity()
        {
            return View("~/Views/Admin/add-city.cshtml", new CityViewModel());
        }

        [HttpPost("add-suburb")]
        public IActionResult AddSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertSuburb", new
                    {
                        SuburbName = model.SuburbName,
                        PostalCode = model.PostalCode,
                        CityId = model.CityId
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Suburbs");
            }
            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);
            return View("~/Views/Admin/add-suburb.cshtml", model);
        }

        [HttpPost("edit-suburb")]
        public IActionResult EditSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateSuburb", new
                    {
                        SuburbID = model.SuburbID,
                        SuburbName = model.SuburbName,
                        PostalCode = model.PostalCode,
                        CityId = model.CityId
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Suburbs");
            }
            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);
            return View("~/Views/Admin/edit-suburb.cshtml", model);
        }

        [HttpPost("delete-suburb/{id}")]
        public IActionResult DeleteSuburb(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteSuburb", new
                {
                    SuburbID = id
                }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Suburbs");
        }

        [HttpGet("suburbs")]
        public IActionResult Suburbs()
        {
            var model = new List<SuburbViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<SuburbViewModel>("GetSuburb", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            return View("~/Views/Admin/suburbs.cshtml", model);
        }

        [HttpGet("conditions")]
        public IActionResult Conditions()
        {
            var model = new List<ConditionViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<ConditionViewModel>("GetConditions", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            return View("~/Views/Admin/conditions.cshtml", model);
        }


        [HttpGet("add-condition")]
        public IActionResult AddCondition()
        {
            return View("~/Views/Admin/add-condition.cshtml", new ConditionViewModel());
        }

        [HttpPost("add-condition")]
        public IActionResult AddCondition(ConditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertChronicCondition", new
                    {
                        Name = model.Name,
                        Description = model.Description
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Conditions");
            }
            return View("~/Views/Admin/add-condition.cshtml", model);
        }

        [HttpGet("edit-condition/{id}")]
        public IActionResult EditCondition(int id)
        {
            var model = new ConditionViewModel();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<ConditionViewModel>("GetChronicCondition", new { ConditionID = id }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    return RedirectToAction("Conditions");
                }
                model = result;
            }
            return View("~/Views/Admin/edit-condition.cshtml", model);
        }

        [HttpPost("edit-condition")]
        public IActionResult EditCondition(ConditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateChronicCondition", new
                    {
                        ConditionID = model.ConditionID,
                        Name = model.Name,
                        Description = model.Description
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Conditions");
            }
            return View("~/Views/Admin/edit-condition.cshtml", model);
        }

        [HttpPost("delete-condition/{id}")]
        public IActionResult DeleteCondition(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteChronicCondition", new { ConditionID = id }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Conditions");
        }

        [HttpPost("add-officemanager")]
        public IActionResult AddOfficeManager(OfficeManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertUser", new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "O",
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value

                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("OfficeManagers");
            }
            return View("~/Views/Admin/add-officemanager.cshtml", model);
        }
        [HttpPost("edit-officemanager")]
        public IActionResult EditOfficeManager(OfficeManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateUser", new
                    {
                        UserID = model.UserID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "O",
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value

                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("OfficeManagers");
            }
            return View("~/Views/Admin/edit-officemanager.cshtml", model);
        }

        [HttpGet("officemanagers")]
        public IActionResult OfficeManagers(int userId)
        {
            var model = new List<OfficeManagerViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<OfficeManagerViewModel>("GetUser", new
                {
                    UserID = userId
                }, commandType: CommandType.StoredProcedure);
                model = results.Where(o => o.UserType == "O").ToList();
            }
            return View("~/Views/Admin/officemanagers.cshtml", model);
        }
        [HttpPost("add-nurse")]
        public IActionResult AddNurse(NurseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertNurse", new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "N",
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        IDNo = model.IDNo
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Nurses");
            }
            return View("~/Views/Admin/add-nurse.cshtml", model);
        }
        [HttpPost("edit-nurse")]
        public IActionResult EditNurse(NurseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateNurse", new
                    {
                        UserID = model.UserID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value,

                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        IDNo = model.IDNo
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Nurses");
            }
            return View("~/Views/Admin/edit-nurse.cshtml", model);
        }
        [HttpPost("delete-nurse")]
        public IActionResult DeleteNurse(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteNurse", new
                {
                    UserID = userId
                }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Nurses");
        }
        [HttpGet("nurses")]
        public IActionResult Nurses()
        {
            var model = new List<NurseViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<NurseViewModel>("GetNurses", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            return View("~/Views/Admin/nurses.cshtml", model);
        }
        [HttpGet("patients")]
        public IActionResult Patients()
        {
            List<PatientViewModel> model;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<PatientViewModel>("GetPatients", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            return View("~/Views/Admin/patients.cshtml", model);
        }
        [HttpPost("profile")]
        public IActionResult Profiles(AdminProfileViewModel model)
        {
            // Implementation
            return View("~/Views/Admin/profile.cshtml", model);
        }
    }
}
