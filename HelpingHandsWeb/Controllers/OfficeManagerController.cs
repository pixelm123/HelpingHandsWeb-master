using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels;
using System;
using HelpingHandsWeb.Data;



namespace HelpingHandsWeb.Controllers
{
  

    namespace HelpingHandsWeb.Controllers
    {
        public class OfficeManagerController : Controller
        {
            public IActionResult OfficeManagerDashboard()
            {
                return View();
            }

            public IActionResult ManageChronicConditions()
            {
                var model = new ManageChronicConditionViewModel();
                model.GetChronicConditions();
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult AddChronicCondition(ManageChronicConditionViewModel newCondition)
            {
                if (ModelState.IsValid)
                {
                    newCondition.AddChronicCondition(newCondition);
                    return RedirectToAction("ManageChronicConditions");
                }
                return View("ManageChronicConditions", newCondition);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult UpdateChronicCondition(ManageChronicConditionViewModel updatedCondition)
            {
                if (ModelState.IsValid)
                {
                    updatedCondition.UpdateChronicCondition(updatedCondition);
                    return RedirectToAction("ManageChronicConditions");
                }
                return View("ManageChronicConditions", updatedCondition);
            }

            public IActionResult DeleteChronicCondition(int conditionId)
            {
                var model = new ManageChronicConditionViewModel();
                model.DeleteChronicCondition(conditionId);
                return RedirectToAction("ManageChronicConditions");
            }

            public IActionResult ManageBusinesses()
            {
                var model = new ManageBusinessViewModel();
                int businessID = 1; 
                model.GetBusinessInfo(businessID);

                return View(model);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult AddBusiness(ManageBusinessViewModel newBusiness)
            {
                if (ModelState.IsValid)
                {
                    newBusiness.AddBusiness();
                    return RedirectToAction("ManageBusinesses");
                }
                return View("ManageBusinesses", newBusiness);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult UpdateBusiness(ManageBusinessViewModel updatedBusiness)
            {
                if (ModelState.IsValid)
                {
                    updatedBusiness.UpdateBusiness(updatedBusiness.BusinessID); 
                    return RedirectToAction("ManageBusinesses");
                }
                return View("ManageBusinesses", updatedBusiness);
            }




            public IActionResult ManageNurses()
            {
                var model = new NurseViewModel();
                model.GetNurse();
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult RegisterNurse(NurseViewModel newNurse)
            {
                if (ModelState.IsValid)
                {
                    newNurse.RegisterNurse();
                    return RedirectToAction("ManageNurses");
                }
                return View("ManageNurses", newNurse);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult UpdateNurse(NurseViewModel updatedNurse)
            {
                if (ModelState.IsValid)
                {
                    
                    updatedNurse.UpdateNurse(updatedNurse.NurseID);
                    return RedirectToAction("ManageNurses");
                }
                return View("ManageNurses", updatedNurse);
            }



            public IActionResult DeleteNurse(int nurseId)
            {
                var model = new NurseViewModel();
                model.DeleteNurse(nurseId);
                return RedirectToAction("ManageNurses");
            }

            public IActionResult AssignCareContract()
            {
                var model = new AssignCareContractViewModel();
                
                return View(model);
            }

            private readonly ApplicationDbContext _dbContext; 

            public OfficeManagerController(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            
            public IActionResult ViewNewCareContracts()
            {
                var viewModel = new AssignCareContractViewModel();
                viewModel.GetNewCareContracts(_dbContext);

                return View(viewModel);
            }

           
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult AssignCareContract(AssignCareContractViewModel assignedContract)
            {
                if (ModelState.IsValid)
                {
                    assignedContract.AssignCareContract(_dbContext);
                    return RedirectToAction("ViewNewCareContracts");
                }

             
                return View("ViewNewCareContracts", assignedContract);
            }

          
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                base.Dispose(disposing);
            }
        



        public IActionResult OfficeManagerIndex()
            {
                var model = new OfficeManagerIndexViewModel();
                return View(model);
            }
        }


    }

}
