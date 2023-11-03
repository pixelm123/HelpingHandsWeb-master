using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientIndexViewModel
    {
        private readonly string _connectionString;

        public int TotalOfficeManagers { get; set; }
        public int TotalPatients { get; set; }
        public int TotalNurses { get; set; }
        public int TotalCities { get; set; }
        public int TotalSuburbs { get; set; }
        public string UserDisplayName { get; set; }

        public PatientIndexViewModel(string userDisplayName, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserDisplayName = userDisplayName;

            TotalOfficeManagers = GetTotal("GetTotalOfficeManagers");
            TotalPatients = GetTotal("GetTotalPatients");
            TotalNurses = GetTotal("GetTotalNurses");
            TotalCities = GetTotal("GetTotalCities");
            TotalSuburbs = GetTotal("GetTotalSuburbs");
        }

        public PatientIndexViewModel(string userDisplayName)
        {
            UserDisplayName = userDisplayName;
        }

        private int GetTotal(string storedProcedureName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<int>(storedProcedureName, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
