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




        [HttpGet]
        public async Task<IActionResult> OfficeManagers()
        {

            var userDisplayName = GetUserDisplayName();
            var officeManagers = await _context.Users
                .Where(u => u.UserType == "O") 
                .Select(u => new OfficeManagerViewModel
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Email = u.Email,
                    ContactNo = u.ContactNo,
                    Status = u.Status
                })
                .ToListAsync();

            return View(officeManagers);
        }

        [HttpGet]
        public async Task<IActionResult> Patients()
        {
           
            var patients = await _context.Patients
                .Where(p => !p.IsDeleted) 
                .Select(p => new PatientViewModel
                {
                    PatientID = p.PatientID,
                    FirstName = p.FirstName,
                    Surname = p.Surname,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth.ToString(),
                    ChronicConditions = p.ChronicConditions,
                    Status = _context.Users
                        .FirstOrDefault(u => u.UserID == p.PatientID && u.UserType == "P"),
                   
                    Condition = _context.PatientConditions
                        .Include(pc => pc.Condition)
                        .Where(pc => pc.PatientID == p.PatientID )
                        .Select(pc => pc.Condition.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return View(patients);
        }
        [HttpGet]
        public async Task<IActionResult> Nurses()
        {
            var nurses = await _context.Users
                .Where(u => u.UserType == "N")
                .Join(
                    _context.Nurses,
                    u => u.UserID,
                    n => n.NurseID,
                    (u, n) => new NurseViewModel
                    {
                        NurseID = n.NurseID,
                        FirstName = n.FirstName,
                        Surname = n.Surname,
                        Gender = n.Gender.ToString(),
                        Email = u.Email,
                        ContactNo = u.ContactNo,
                        Status = u.Status,
                        PreferredSuburb = _context.PreferredSuburbs
                            .Where(ps => ps.NurseID == n.NurseID)
                            .Select(ps => ps.Suburb.SuburbName)
                            .FirstOrDefault()
                    })
                .ToListAsync();

            return View(nurses);
        }


        [HttpGet]
        public async Task<IActionResult> Suburbs()
        {
            var suburbs = await _context.Suburbs
                .Include(s => s.City)
                .Select(s => new SuburbViewModel
                {
                    SuburbId = s.SuburbID,
                    SuburbName = s.SuburbName,
                    PostalCode = s.PostalCode,
                    CityName = s.City.Name
                })
                .ToListAsync();

            return View(suburbs);
        }
    }
}
