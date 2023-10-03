using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class ManageSuburbsViewModel
    {
        public int SuburbId { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }

        public List<ManageSuburbsViewModel> Suburbs { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public ManageSuburbsViewModel()
        {
            Suburbs = new List<ManageSuburbsViewModel>();
        }

        public void GetSuburbs()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetSuburb", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SuburbId", null);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ManageSuburbsViewModel suburb = new ManageSuburbsViewModel
                        {
                            SuburbId = Convert.ToInt32(reader["SuburbId"]),
                            Suburb = reader["Suburb"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            CityName = reader["CityName"].ToString()
                        };
                        Suburbs.Add(suburb);
                    }
                }
            }
        }

        public void AddSuburb(ManageSuburbsViewModel newSuburb)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertSuburb", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Suburb", newSuburb.Suburb);
                    cmd.Parameters.AddWithValue("@PostalCode", newSuburb.PostalCode);
                    cmd.Parameters.AddWithValue("@CityName", newSuburb.CityName); 
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSuburb(ManageSuburbsViewModel updatedSuburb)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateSuburb", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SuburbId", updatedSuburb.SuburbId);
                    cmd.Parameters.AddWithValue("@Suburb", updatedSuburb.Suburb);
                    cmd.Parameters.AddWithValue("@PostalCode", updatedSuburb.PostalCode);
                    cmd.Parameters.AddWithValue("@CityName", updatedSuburb.CityName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSuburb(int suburbId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteSuburb", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SuburbId", suburbId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
