  using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class AdminIndexViewModel
    {
        private readonly string _connectionString;

        public string UserName { get; set; }
        public int TotalOfficeManagers { get; set; }
        public int TotalPatients { get; set; }
        public int TotalNurses { get; set; }
        public int TotalChronicConditions { get; set; }

        public List<PatientViewModel> Patients { get; set; }
        public List<NurseViewModel> Nurses { get; set; }

        public AdminIndexViewModel(string userName, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserName = userName;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                TotalOfficeManagers = connection.QueryFirstOrDefault<int>("GetTotalOfficeManagers", commandType: CommandType.StoredProcedure);
                TotalPatients = connection.QueryFirstOrDefault<int>("GetPatients", commandType: CommandType.StoredProcedure);
                TotalNurses = connection.QueryFirstOrDefault<int>("GetTotalNurses", commandType: CommandType.StoredProcedure);
                TotalChronicConditions = connection.QueryFirstOrDefault<int>("GetTotalChronicConditions", commandType: CommandType.StoredProcedure);
            }
        }

        
    }
}