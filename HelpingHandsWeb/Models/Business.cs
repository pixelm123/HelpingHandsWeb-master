using System;
using System.Collections.Generic;

namespace HelpingHandsWeb.Models
{
    public partial class Business
    {
        public int BusinessId { get; set; }
        public string? OrganizationName { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? OperatingHours { get; set; }
        public bool IsDeleted { get; set; }
        public byte[]? Logo { get; set; }
    }
}
