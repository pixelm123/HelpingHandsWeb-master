using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels;
using HelpingHandsWeb.Models.ViewModels.NurseViewModels;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using HelpingHandsWeb.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace HelpingHandsWeb.Controllers
{
    public class RegisterController : Controller
    {

        public IActionResult Register()
        {
            var model = new RegisterViewModel(); 
            return View("~/Views/Home/register.cshtml", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    model.RegisterPatient();

                    
                    model.RegistrationSuccess = true;

                    return RedirectToAction("PatientDashboard", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred during registration: " + ex.Message);
                   
                    model.RegistrationSuccess = false;
                }
            }

           
            model.AvailableChronicConditions = GetChronicConditions(); 
            return View("~/Views/Home/register.cshtml", model);
        }

        private List<ManageChronicConditionViewModel> GetChronicConditions()
        {
            var chronicConditionViewModel = new ManageChronicConditionViewModel();
            List<ManageChronicConditionViewModel> conditions = chronicConditionViewModel.GetChronicConditions();
            return conditions;
        }




    }
}
