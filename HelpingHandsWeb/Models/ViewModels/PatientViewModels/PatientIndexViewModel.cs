using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration; // Add this using statement for IConfiguration

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientIndexViewModel
    {
        private readonly string _connectionString;

        public string UserDisplayName { get; set; }
        public int TotalPatientCareVisits { get; set; }
        public int TotalPatientCareContracts { get; set; }
        public List<PatientConditionsViewModel> PatientConditions { get; set; }
        public List<PatientAppointmentsViewModel> PatientAppointments { get; set; }

        public PatientIndexViewModel(string userDisplayName, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserDisplayName = userDisplayName;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                TotalPatientCareVisits = connection.QueryFirstOrDefault<int>("GetTotalCareVisits", commandType: CommandType.StoredProcedure);
                TotalPatientCareContracts = connection.QueryFirstOrDefault<int>("GetTotalCareContracts", commandType: CommandType.StoredProcedure);
            }
        }

        // Add a constructor to initialize properties with data from the database
        public PatientIndexViewModel(string userDisplayName, IConfiguration configuration, int totalPatientCareVisits, int totalPatientCareContracts, List<PatientConditionsViewModel> patientConditions, List<PatientAppointmentsViewModel> patientAppointments)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserDisplayName = userDisplayName;
            TotalPatientCareVisits = totalPatientCareVisits;
            TotalPatientCareContracts = totalPatientCareContracts;
            PatientConditions = patientConditions;
            PatientAppointments = patientAppointments;
        }
    }
}
