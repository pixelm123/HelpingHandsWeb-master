using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientProfileViewModel
    {

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmergencyPerson { get; set; }
        public string EmergencyContactNo { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public string ConfirmNewPassword { get; set; }

        public string ContactNo { get; set; }

        public string Password { get; set; }

        public byte[] ProfilePicture { get; set; }

        public IEnumerable<PatientConditionsViewModel> ChronicConditions { get; set; }



    }
}
