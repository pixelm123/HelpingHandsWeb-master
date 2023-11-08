using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Nurse
    {
        public Nurse()
        {
            Appointments = new HashSet<Appointment>();
            CareContracts = new HashSet<CareContract>();
            PreferredSuburbs = new HashSet<PreferredSuburb>();
        }

        public int NurseId { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        public string? Idno { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<CareContract> CareContracts { get; set; }
        public virtual ICollection<PreferredSuburb> PreferredSuburbs { get; set; }
    }
}
