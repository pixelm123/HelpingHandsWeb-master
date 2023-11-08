using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels
{
    public class AssignCareContractViewModel
    {
        public int ContractID { get; set; }


        [Required(ErrorMessage = "Please select a nurse.")]
        [Display(Name = "Nurse")]
        public int NurseId { get; set; }

        [Required(ErrorMessage = "Please specify the Start Care Date.")]
        [Display(Name = "Start Care Date")]
        [DataType(DataType.Date)]
        public DateTime StartCareDate { get; set; }

        [Display(Name = "End Care Date")]
        [DataType(DataType.Date)]
        public DateTime? EndCareDate { get; set; }

        
        public List<CareContract> NewCareContracts { get; set; }

        public IEnumerable<SelectListItem> NurseList { get; set; }

        public AssignCareContractViewModel()
        {
            NurseList = new List<SelectListItem>();
            NewCareContracts = new List<CareContract>();
        }

        
        public void GetNewCareContracts(DbContext dbContext)
        {

            NewCareContracts = dbContext.Set<CareContract>()
                .Where(c => c.ContractStatus == "New")
                .ToList();
        }

     
        public void AssignCareContract(DbContext dbContext)
        {
            
            var careContract = dbContext.Set<CareContract>().Find(ContractID);

            if (careContract != null)
            {
                
                careContract.NurseId = NurseId;
                careContract.ContractStatus = "Assigned";
                careContract.StartCareDate = StartCareDate;
                careContract.EndCareDate = EndCareDate;

                
                dbContext.SaveChanges();
            }
        }
    }
}
