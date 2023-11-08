using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            CareContracts = new HashSet<CareContract>();
            CareVisits = new HashSet<CareVisit>();
            PatientConditions = new HashSet<PatientCondition>();
        }

        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmergencyPerson { get; set; }
        public string? EmergencyContactNo { get; set; }
        public bool? IsDeleted { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CareContract> CareContracts { get; set; }
        public virtual ICollection<CareVisit> CareVisits { get; set; }
        public virtual ICollection<PatientCondition> PatientConditions { get; set; }
    }
}
