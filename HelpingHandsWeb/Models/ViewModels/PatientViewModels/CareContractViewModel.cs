using System;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class CareContractViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Patient First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Patient Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter the first line of your address.")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter your suburb.")]
        public int SuburbId { get; set; }

        [Display(Name = "Chronic Conditions")]
        public string ChronicConditions { get; set; }

        [Required(ErrorMessage = "Please provide a description of your wound.")]
        [Display(Name = "Wound Description")]
        public string WoundDescription { get; set; }

        [Display(Name = "Contract Date")]
        public DateTime ContractDate { get; set; }
    }
}
