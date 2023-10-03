using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models
{
    public class Business
    {
        [Key]
        public int BusinessID { get; set; }

        [Required(ErrorMessage = "Organization Name is required.")]
        [StringLength(100)]
        public string OrganizationName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string OperatingHours { get; set; }


        public bool IsDeleted { get; set; }
    }
}
