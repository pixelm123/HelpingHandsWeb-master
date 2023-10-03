using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.NurseViewModels
{
    public class CloseCareContractViewModel
    {
        public int ContractID { get; set; }

        [Required(ErrorMessage = "Please enter the end care date.")]
        [DataType(DataType.Date)]
        public DateTime EndCareDate { get; set; }

        public string Notes { get; set; }
    }

}
