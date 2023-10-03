using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required.")]
        [StringLength(10)]
        public string Abbreviation { get; set; }

        public bool IsDeleted { get; set; }
    }
}
