using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class CareVisit
    {
        public int VisitId { get; set; }
        public int ContractId { get; set; }
        public int PatientId { get; set; }
        public DateTime? VisitDate { get; set; }
        public TimeSpan? ApproxArriveTime { get; set; }
        public TimeSpan? ArriveTime { get; set; }
        public TimeSpan? DepartTime { get; set; }
        public string? WoundCondition { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }

        public virtual CareContract Contract { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
