using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class OfficeManagerViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        public string UserType { get; set; }
        public OfficeManagerViewModel()
        {
            Status = "A";
            UserType = "O";

        }
    }

}


