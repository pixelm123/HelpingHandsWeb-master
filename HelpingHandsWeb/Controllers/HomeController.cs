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

       

        public IActionResult PatientDashboard()
        {
           
            return View();
        }

        public IActionResult AdminDashboard()
        {

            return View();
        }


        public IActionResult OfficeManagerRegistrationPage()
        {

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}