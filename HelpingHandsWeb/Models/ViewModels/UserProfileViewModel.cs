using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels
{
    public class UserProfileViewModel
    {
       
            [Required(ErrorMessage = "Username is required.")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Contact number is required.")]
         
            public string ContactNumber { get; set; }

            public string UserType { get; set; } 
    }
}
