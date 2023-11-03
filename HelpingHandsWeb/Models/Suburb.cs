using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
    public class Suburb
    {
        [Key]
        public int SuburbID { get; set; }

        [Required(ErrorMessage = "Suburb Name is required.")]
        [StringLength(50)]
        public string SuburbName { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        //public int CityId { get; set; }

        [ForeignKey("CityID")]
        public int CityId { get; set; }

        public City City { get; set; }
        public bool IsDeleted { get; set; }
    }
}
