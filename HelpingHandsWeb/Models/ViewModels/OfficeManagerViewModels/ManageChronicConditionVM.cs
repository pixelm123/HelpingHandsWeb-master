using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels
{
    public class ManageChronicConditionViewModel
    {
        public int ConditionID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
		public readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";



		public List<ManageChronicConditionViewModel> GetChronicConditions()
        {
            List<ManageChronicConditionViewModel> conditions = new List<ManageChronicConditionViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetChronicCondition", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            conditions.Add(new ManageChronicConditionViewModel
                            {
                                ConditionID = (int)reader["ConditionID"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsDeleted = (bool)reader["IsDeleted"]
                            });
                        }
                    }
                }
            }

            return conditions;
        }

        
        public void AddChronicCondition(ManageChronicConditionViewModel newCondition)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertChronicCondition", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", newCondition.Name);
                    command.Parameters.AddWithValue("@Description", newCondition.Description);
                    command.Parameters.AddWithValue("@IsDeleted", false); 
                    command.ExecuteNonQuery();
                }
            }
        }

        
        public void UpdateChronicCondition(ManageChronicConditionViewModel updatedCondition)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateChronicCondition", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ConditionID", updatedCondition.ConditionID);
                    command.Parameters.AddWithValue("@Name", updatedCondition.Name);
                    command.Parameters.AddWithValue("@Description", updatedCondition.Description);
                    command.ExecuteNonQuery();
                }
            }
        }

       
        public void DeleteChronicCondition(int conditionId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteChronicCondition", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ConditionID", conditionId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
