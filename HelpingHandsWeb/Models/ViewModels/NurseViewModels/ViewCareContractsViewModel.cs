
using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels.NurseViewModels;

namespace HelpingHandsWeb.Models.ViewModels.NurseViewModels.ViewCareContractsViewModel
{
    public class NurseController : Controller
    {
       
        public IActionResult CloseCareContract()
        {
            var model = new CloseCareContractViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CloseCareContract(CloseCareContractViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
            }
            return View(viewModel);
        }

       
        public IActionResult ManageCareVisits()
        {
            var model = new ManageCareVisitsViewModel();
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageCareVisits(ManageCareVisitsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Implement the logic to manage care visits
                // You can access properties from viewModel here
                // Redirect to a relevant page after successful update
            }
            return View(viewModel);
        }

        // Action for choosing suburbs
        public IActionResult ChooseSuburbs()
        {
            var model = new ChooseSuburbsViewModel();
            // Load data into model.Suburbs if needed
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseSuburbs(ChooseSuburbsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Implement the logic for choosing suburbs
                // You can access properties from viewModel here
                // Redirect to a relevant page after successful selection
            }
            return View(viewModel);
        }

        // Action for updating nurse profile
        public IActionResult UpdateProfile()
        {
            var model = new UpdateProfileViewModel();
            // Load data into model if needed for pre-populating the form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UpdateProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Implement the logic to update nurse profile
                // You can access properties from viewModel here
                // Redirect to a relevant page after successful update
            }
            return View(viewModel);
        }

        //// Action for viewing care contracts
        //public IActionResult ViewCareContracts()
        //{
        //    var model = new List<ViewCareContract>();
        //    // Load data into model if needed
        //    return View(model);
        //}
    }
}
