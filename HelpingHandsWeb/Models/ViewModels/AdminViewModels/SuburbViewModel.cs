using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class SuburbViewModel
    {
        [Key]
        public int SuburbID { get; set; }

        [Required(ErrorMessage = "Suburb Name is required.")]
        [StringLength(50)]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }
        public string CityAbbreviation { get; set; }

        public bool IsDeleted { get; set; }
    }
}
