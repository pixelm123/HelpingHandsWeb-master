using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class ManagePatientsViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }

        public List<ManagePatientsViewModel> Patients { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public ManagePatientsViewModel()
        {
            Patients = new List<ManagePatientsViewModel>();
        }

        public void GetActivePatients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetActivePatients", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ManagePatientsViewModel patient = new ManagePatientsViewModel
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            ContactNo = reader["ContactNo"].ToString()
                        };
                        Patients.Add(patient);
                    }
                }
            }
        }

        public void DeletePatient(int patientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdatePatientStatus", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", patientId);
                    cmd.Parameters.AddWithValue("@Status", "I"); // Set status to Inactive
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
