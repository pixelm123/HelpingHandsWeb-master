using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email address or username is required.")]
        [StringLength(100, ErrorMessage = "Email address or username cannot exceed 100 characters.")]
        public string EmailOrUsername { get; set; }
    }
}
