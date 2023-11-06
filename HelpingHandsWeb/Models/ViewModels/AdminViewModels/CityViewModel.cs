using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class CityViewModel
    {

        private readonly string _connectionString;
        
        public int CityId { get; set; }

        [Required(ErrorMessage = "City Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Abbreviation is required.")]
        public string Abbreviation { get; set; }

       
    }
}



