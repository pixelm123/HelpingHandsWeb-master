using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class CareContract
    {
        public CareContract()
        {
            CareVisits = new HashSet<CareVisit>();
        }

        public int ContractId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? ContractDate { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? WoundDescription { get; set; }
        public DateTime? StartCareDate { get; set; }
        public DateTime? EndCareDate { get; set; }
        public int? NurseId { get; set; }
        public string? ContractStatus { get; set; }
        public bool IsDeleted { get; set; }
        public int? SuburbId { get; set; }

        public virtual Nurse? Nurse { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual Suburb? Suburb { get; set; }
        public virtual ICollection<CareVisit> CareVisits { get; set; }
    }
}
