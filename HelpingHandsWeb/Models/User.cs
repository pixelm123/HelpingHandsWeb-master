using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class User
    {
        public User()
        {
            Nurses = new HashSet<Nurse>();
            Patients = new HashSet<Patient>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string UserType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public virtual Usertype UserTypeNavigation { get; set; } = null!;
        public virtual ICollection<Nurse> Nurses { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
