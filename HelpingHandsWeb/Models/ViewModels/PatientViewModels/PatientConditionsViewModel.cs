using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;


namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientConditionsViewModel
    {

        [Key]
        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public string ConditionName { get; set; }
    }
}
