using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class AdminIndexViewModel
    {
        public int TotalOfficeManagers { get; set; }
        public int TotalPatients { get; set; }
        public int TotalNurses { get; set; }
        public int TotalCities { get; set; }
        public int TotalSuburbs { get; set; }
        public string UserDisplayName { get; set; }


        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public AdminIndexViewModel(string userDisplayName)
        {
            UserDisplayName = userDisplayName;
            TotalOfficeManagers = GetTotalOfficeManagers();
            TotalPatients = GetTotalPatients();
            TotalNurses = GetTotalNurses();
            TotalCities = GetTotalCities();
            TotalSuburbs = GetTotalSuburbs();

            UserDisplayName = userDisplayName;
        }

        private int GetTotalOfficeManagers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTotalOfficeManagers", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetTotalPatients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTotalPatients", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetTotalNurses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTotalNurses", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetTotalCities()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTotalCities", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        private int GetTotalSuburbs()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetTotalSuburbs", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
