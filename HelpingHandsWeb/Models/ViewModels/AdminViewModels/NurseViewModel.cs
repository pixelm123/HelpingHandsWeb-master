using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class NurseViewModel
    {
        public List<NurseViewModel> Nurses { get; set; }
        public int UserID { get; set; }
        public int NurseID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [Phone(ErrorMessage = "Invalid Contact Number.")]
        public string ContactNo { get; set; }

        public string UserType { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        public byte[] ProfilePicture { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "ID Number is required.")]
        public string IDNo { get; set; }

        private readonly string _connectionString;

       
    }
}
