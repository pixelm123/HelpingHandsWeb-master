using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models.Users
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Emergency Person is required.")]
        public string EmergencyPerson { get; set; }

        [Required(ErrorMessage = "Emergency Contact Number is required.")]
        [Phone(ErrorMessage = "Invalid Emergency Contact Number.")]
        public string EmergencyContactNo { get; set; }

        public bool IsDeleted { get; set; }

       
        [ForeignKey("User")]
        public int? UserID { get; set; }

       
        public virtual User User { get; set; }
    }
}
