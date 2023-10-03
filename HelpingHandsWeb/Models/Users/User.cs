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

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50)]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "User Type is required.")]
        [StringLength(1)]
        public string UserType { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(1)]
        public char Status { get; set; }


        public bool IsDeleted { get; set; }
    }
}
