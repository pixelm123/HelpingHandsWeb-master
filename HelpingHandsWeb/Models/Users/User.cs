using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models.Users
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

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

        [ForeignKey("UserType")]
        public string UserTypeId { get; set; } 

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] ProfilePicture { get; set; }

        // Navigation Properties
        public virtual Patient Patient { get; set; }

        // Rename this navigation property to UserType
        public virtual UserType UserType { get; set; }


    }
}
