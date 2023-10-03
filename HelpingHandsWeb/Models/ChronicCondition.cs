using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
    public class ChronicCondition
    {
        [Key]
        public int ConditionID { get; set; }

        [Required(ErrorMessage = "Condition Name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(maximumLength: int.MaxValue)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
