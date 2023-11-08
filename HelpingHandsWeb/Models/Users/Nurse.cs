using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHandsWeb.Models.Users
{
    public class Nurse
    {
        [Key]
        public int NurseId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        [StringLength(20)]
        public string IDNo { get; set; }

        public bool IsDeleted { get; set; }

        public List<Patient> Patients { get; set; }
    }
}
