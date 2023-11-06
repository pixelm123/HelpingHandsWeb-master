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
        public int PatientID { get; set; }
        public int ConditionID { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
