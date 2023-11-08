using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class PreferredSuburb
    {
        public int PrefferredSuburb { get; set; }
        public int? NurseId { get; set; }
        public int? SuburbId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Nurse? Nurse { get; set; }
        public virtual Suburb? Suburb { get; set; }
    }
}
