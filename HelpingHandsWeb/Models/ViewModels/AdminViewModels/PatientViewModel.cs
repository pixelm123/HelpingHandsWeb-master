using System;
using System.Collections.Generic;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class PatientViewModel
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public List<ChronicCondition> ChronicConditions { get; set; }
        public User User { get; set; }
        public User Status { get; set; }
        public string Condition { get; set; }

        public string UserDisplayName { get; set; }

    }
}