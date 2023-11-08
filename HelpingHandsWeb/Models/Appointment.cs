using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int NurseId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? Title { get; set; }
        public TimeSpan? Time { get; set; }
        public int? Duration { get; set; }

        public virtual Nurse Nurse { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
