using HelpingHandsWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HelpingHandsWeb.Controllers;


namespace HelpingHandsWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        public IActionResult Service()
        {

            return View();
        }

        
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

      
        public IActionResult AdminDashboard()
        {

            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult AddCity()
        {
            return View();
        }
        public IActionResult EditCity()
        {
           return View();    
        }
        public IActionResult DeleteCity()
        {
           return View();
        }
        public IActionResult AddCondition()
        {
            return View();
        }
        public IActionResult EditCondition()
        {
            return View();
        }
        public IActionResult DeleteCondition()
        {
            return View();
        }
        public IActionResult Nurses()
        {
            return View();
        }

        public IActionResult EditNurse()
        {
            return View();
        }

        public IActionResult DeleteNurse()
        {
            return View();
        }

        public IActionResult OfficeManagers()
        {
            return View();
        }

        public IActionResult AddOfficeManager()
        {
            return View();
        }

        public IActionResult EditOfficeManager()
        {
            return View();
        }

        public IActionResult Suburbs()
        {
            return View();
        }

        public IActionResult AddSuburb()
        {
            return View();
        }
        public IActionResult EditSuburb()
        {
            return View();
        }

        public IActionResult DeleteSuburb()
        {
            return View();
        }

        public IActionResult Patients()
        {
            return View();
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}