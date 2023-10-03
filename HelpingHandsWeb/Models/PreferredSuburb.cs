using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
    public class PreferredSuburb
{
    public int NurseID { get; set; }

    [ForeignKey("NurseID")]
    public Nurse Nurse { get; set; }

    public int SuburbId { get; set; }

    [ForeignKey("SuburbID")]
    public Suburb Suburb { get; set; }

    public bool IsDeleted { get; set; }
}
}
