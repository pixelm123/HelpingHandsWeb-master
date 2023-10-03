using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HelpingHandsWeb.Controllers
{
    public class PatientController : Controller
    {
        private readonly string _connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;Trusted_Connection=True;MultipleActiveResultSets=true";

       


        public IActionResult PatientDashboard()
        {
            var model = new PatientIndexViewModel();
       
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
              
            }
            return View(viewModel);
        }

        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(viewModel);
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.RegisterPatient();
                return RedirectToAction("RegistrationSuccess"); 
            }
            return View(viewModel);
        }


        public IActionResult RequestCareContract()
        {
            var model = new RequestCareContractViewModel();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestCareContract(RequestCareContractViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
            }
            return View(viewModel);
        }

        public IActionResult UpdateProfile()
        {
            var model = new UpdateProfileViewModel();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UpdateProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
            }
            return View(viewModel);
        }
    }
}
