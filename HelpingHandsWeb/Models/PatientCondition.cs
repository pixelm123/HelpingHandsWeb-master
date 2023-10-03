using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;


namespace HelpingHandsWeb.Models
{
    public class PatientCondition
    {
        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }

        public int ConditionID { get; set; }

        [ForeignKey("ConditionID")]
        public ChronicCondition Condition { get; set; }

        public bool IsDeleted { get; set; }
    }
}
