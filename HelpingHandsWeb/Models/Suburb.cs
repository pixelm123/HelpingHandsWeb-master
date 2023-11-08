using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Suburb
    {
        public Suburb()
        {
            CareContracts = new HashSet<CareContract>();
            PreferredSuburbs = new HashSet<PreferredSuburb>();
        }

        public int SuburbId { get; set; }
        public string Suburb1 { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public int CityId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<CareContract> CareContracts { get; set; }
        public virtual ICollection<PreferredSuburb> PreferredSuburbs { get; set; }
    }
}
