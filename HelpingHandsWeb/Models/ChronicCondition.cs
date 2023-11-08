using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class ChronicCondition
    {
        public ChronicCondition()
        {
            PatientConditions = new HashSet<PatientCondition>();
        }

        public int ConditionId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PatientCondition> PatientConditions { get; set; }
    }
}
