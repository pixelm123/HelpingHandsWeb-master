using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.NurseViewModels
{
    public class ManageCareVisitsViewModel
    {

        public int ContractID { get; set; }
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please enter the visit date.")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        [Required(ErrorMessage = "Please enter the approximate arrival time.")]
        [DataType(DataType.Time)]
        public TimeSpan ApproxArriveTime { get; set; }


        public int VisitID { get; set; }

        [Required(ErrorMessage = "Please enter the arrival time.")]
        [DataType(DataType.Time)]
        public TimeSpan ArriveTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? DepartTime { get; set; }

        [Required(ErrorMessage = "Please enter the wound condition.")]
        [MaxLength(1000, ErrorMessage = "Wound condition cannot exceed 1000 characters.")]
        public string WoundCondition { get; set; }

        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string Notes { get; set; }


        public List<CareVisitViewModel> CareVisits { get; set; }
    }

    public class CareVisitViewModel
    {
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public TimeSpan ApproxArriveTime { get; set; }
        public TimeSpan? ArriveTime { get; set; }
        public TimeSpan? DepartTime { get; set; }
        public string WoundCondition { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
    }

}
