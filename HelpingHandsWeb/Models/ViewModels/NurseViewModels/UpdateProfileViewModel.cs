using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.NurseViewModels
{
    public class UpdateProfileViewModel
    {
        public int NurseId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Please enter your surname.")]
        public string Surname { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "ID Number")]
        public string IDNo { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Please enter your contact number.")]
        public string ContactNo { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
    }

}
