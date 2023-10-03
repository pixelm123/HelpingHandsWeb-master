using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class ManageCitiesViewModel
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public List<ManageCitiesViewModel> Cities { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public ManageCitiesViewModel()
        {
            Cities = new List<ManageCitiesViewModel>();
        }

        public void GetCities()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetCity", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CityId", null);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ManageCitiesViewModel city = new ManageCitiesViewModel
                        {
                            CityId = Convert.ToInt32(reader["CityId"]),
                            Name = reader["Name"].ToString(),
                            Abbreviation = reader["Abbreviation"].ToString()
                        };
                        Cities.Add(city);
                    }
                }
            }
        }

        public void AddCity(ManageCitiesViewModel newCity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertCity", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", newCity.Name);
                    cmd.Parameters.AddWithValue("@Abbreviation", newCity.Abbreviation);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCity(ManageCitiesViewModel updatedCity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateCity", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CityId", updatedCity.CityId);
                    cmd.Parameters.AddWithValue("@Name", updatedCity.Name);
                    cmd.Parameters.AddWithValue("@Abbreviation", updatedCity.Abbreviation);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCity(int cityId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteCity", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CityId", cityId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
