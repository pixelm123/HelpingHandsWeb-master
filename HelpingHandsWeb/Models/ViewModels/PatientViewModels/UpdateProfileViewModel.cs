using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class UpdateProfileViewModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Select Gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", ErrorMessage = "Email address does not match the expected format (e.g., name@example.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^(?=.*\d)[\d\s-]+$", ErrorMessage = "Phone number should be in the format: 071 234 5678.")]
        [StringLength(15, ErrorMessage = "Contact Number cannot exceed 15 characters.")]
        public string ContactNo { get; set; }

        public string UserType { get; set; }

        
        public List<int> UpdatedChronicConditions { get; set; }
    }
}
