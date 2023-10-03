using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models.Users
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string EmergencyPerson { get; set; }

        [StringLength(15)]
        public string EmergencyContactNo { get; set; }

        public bool IsDeleted { get; set; }

        public List<PatientCondition> PatientConditions { get; set; }
        public List<ChronicCondition> ChronicConditions { get; set; }
    }
}
