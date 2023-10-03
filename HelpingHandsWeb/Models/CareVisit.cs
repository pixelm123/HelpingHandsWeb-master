using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
   public class CareVisit
{
    [Key]
    public int VisitID { get; set; }

    public int ContractID { get; set; }

    [ForeignKey("ContractID")]
    public CareContract CareContract { get; set; }

    public int PatientID { get; set; }

    [ForeignKey("PatientID")]
    public Patient Patient { get; set; }

    public DateTime? VisitDate { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? ApproxArriveTime { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? ArriveTime { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? DepartTime { get; set; }

    [StringLength(maximumLength: int.MaxValue)]
    public string WoundCondition { get; set; }

    [StringLength(maximumLength: int.MaxValue)]
    public string Notes { get; set; }

 
    public bool IsDeleted { get; set; }
}
}
