using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Usertype
    {
        public Usertype()
        {
            Users = new HashSet<User>();
        }

        public string UserTypeId { get; set; } = null!;
        public string? UserTypeDesc { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
