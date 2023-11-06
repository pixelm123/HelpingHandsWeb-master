using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientIndexViewModel
    {
        private readonly string _connectionString;

        public string UserName { get; set; }
        public int PatientId { get; set; }
        public int TotalCareVisits { get; set; }
        public int TotalCareContracts { get; set; }
        public List<PatientConditionsViewModel> PatientConditions { get; set; }
        public List<PatientAppointmentsViewModel> Appointments{ get; set; }


        public PatientIndexViewModel(string userName, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserName = userName;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                TotalCareVisits = connection.QueryFirstOrDefault<int>("GetTotalCareVisits", commandType: CommandType.StoredProcedure);
                TotalCareContracts = connection.QueryFirstOrDefault<int>("GetTotalCareContracts", commandType: CommandType.StoredProcedure);


            }
        }



        //public PatientIndexViewModel(string userDisplayName, IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //    UserDisplayName = userDisplayName;

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        TotalCareVisits = connection.QueryFirstOrDefault<int>("GetTotalCareVisits", commandType: CommandType.StoredProcedure);
        //        TotalCareContracts = connection.QueryFirstOrDefault<int>("GetTotalCareContracts", commandType: CommandType.StoredProcedure);
        //    }
        //}

        //// Add a constructor to initialize properties with data from the database
        //public PatientIndexViewModel(string userDisplayName, IConfiguration configuration, int totalPatientCareVisits, int totalPatientCareContracts, List<PatientConditionsViewModel> patientConditions, List<PatientAppointmentsViewModel> patientAppointments)
        //{
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //    UserDisplayName = userDisplayName;
        //    TotalCareVisits = totalPatientCareVisits;
        //    TotalCareContracts = totalPatientCareContracts;
        //    PatientConditions = patientConditions;
        //    PatientAppointments = patientAppointments;
        //}
    }
}
