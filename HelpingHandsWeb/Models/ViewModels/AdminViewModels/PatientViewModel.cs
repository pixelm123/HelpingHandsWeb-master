using System;
using System.Collections.Generic;
using System.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class PatientViewModel
    {

        public int UserID { get; set; }
        public int PatientId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string UserType { get; set; }
        public string Status { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmergencyPerson { get; set; }
        public string EmergencyContactNo { get; set; }
    }
}


