using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Old Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required.")]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirm New Password do not match. Please try again.")]
        public string ConfirmNewPassword { get; set; }
    }
}
