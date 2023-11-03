using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class AdminIndexViewModel
    {
        private readonly string _connectionString;

        public int TotalOfficeManagers { get; set; }
        public int TotalPatients { get; set; }
        public int TotalNurses { get; set; }
        public int TotalCities { get; set; }
        public int TotalSuburbs { get; set; }
        public string UserDisplayName { get; set; }

        public AdminIndexViewModel(string userDisplayName, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserDisplayName = userDisplayName;

            TotalOfficeManagers = GetTotal("GetTotalOfficeManagers");
            TotalPatients = GetTotal("GetTotalPatients");
            TotalNurses = GetTotal("GetTotalNurses");
            TotalCities = GetTotal("GetTotalCities");
            TotalSuburbs = GetTotal("GetTotalSuburbs");
        }

        public AdminIndexViewModel(string userDisplayName)
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
