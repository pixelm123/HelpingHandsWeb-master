using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels
{
    public class ManageBusinessViewModel
    {
        public int BusinessID { get; set; }

        [Required(ErrorMessage = "Organization Name is required.")]
        [StringLength(100, ErrorMessage = "Organization Name must be no more than 100 characters.")]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address must be no more than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Contact Number must be a 10-digit number.")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Operating Hours are required.")]
        [StringLength(100, ErrorMessage = "Operating Hours must be no more than 100 characters.")]
        [Display(Name = "Operating Hours")]
        public string OperatingHours { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;Trusted_Connection=True;MultipleActiveResultSets=true"; 

        public ManageBusinessViewModel()
        {

        }

        public void GetBusinessInfo(int businessID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("GetBusiness", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", businessID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        BusinessID = Convert.ToInt32(reader["BusinessID"]);
                        OrganizationName = reader["OrganizationName"].ToString();
                        Address = reader["Address"].ToString();
                        ContactNumber = reader["ContactNumber"].ToString();
                        Email = reader["Email"].ToString();
                        OperatingHours = reader["OperatingHours"].ToString();
                    }
                }
            }
        }

        public void AddBusiness()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("InsertBusiness", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrganizationName", this.OrganizationName); 
                    cmd.Parameters.AddWithValue("@Address", this.Address); 
                    cmd.Parameters.AddWithValue("@ContactNumber", this.ContactNumber); 
                    cmd.Parameters.AddWithValue("@Email", this.Email); 
                    cmd.Parameters.AddWithValue("@OperatingHours", this.OperatingHours); 

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateBusiness(int businessID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("UpdateBusiness", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", businessID); 
                    cmd.Parameters.AddWithValue("@OrganizationName", OrganizationName);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@ContactNumber", ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@OperatingHours", OperatingHours);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
