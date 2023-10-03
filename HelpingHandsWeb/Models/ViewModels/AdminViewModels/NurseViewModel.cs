using System;
using System.Collections.Generic;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class NurseViewModel
    {
        public int NurseID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public char Status { get; set; }
        public string PreferredSuburb { get; set; }
        public User user { get; set; }
        public string UserDisplayName { get; set; }
    }
}