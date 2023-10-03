﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;


namespace HelpingHandsWeb.Models
{
    public class CareContract
    {
        [Key]
        public int ContractID { get; set; }

        public int? PatientID { get; set; }

        [ForeignKey("PatientID")]
        public Patient Patient { get; set; }

        public DateTime? ContractDate { get; set; }

        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(50)]
        public string Suburb { get; set; }

        [StringLength(maximumLength: int.MaxValue)]
        public string WoundDescription { get; set; }

        public DateTime? StartCareDate { get; set; }

        public DateTime? EndCareDate { get; set; }

        public int? NurseID { get; set; }

        [ForeignKey("NurseID")]
        public Nurse Nurse { get; set; }

        [StringLength(50)]
        public string ContractStatus { get; set; }


        public bool IsDeleted { get; set; }
    }
}
