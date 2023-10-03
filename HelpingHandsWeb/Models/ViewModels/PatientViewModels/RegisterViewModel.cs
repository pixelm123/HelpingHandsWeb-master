using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class RegisterViewModel
    {
        public int PatientID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Select Gender.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date Of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Emergency contact person is required.")]
        [StringLength(100, ErrorMessage = "Emergency contact person cannot exceed 100 characters.")]
        public string EmergencyPerson { get; set; }

        [Required(ErrorMessage = "Emergency contact number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Emergency contact number should be in the format: 0712345678")]
        [StringLength(10, ErrorMessage = "Emergency contact number must contain only 10 digits.")]
        public string EmergencyContactNo { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed in the username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", ErrorMessage = "Email address does not match the expected format (e.g., name@example.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^(?=.*\d)[\d\s-]+$", ErrorMessage = "Phone number should be in the format: 071 234 5678.")]
        [StringLength(15, ErrorMessage = "Contact Number cannot exceed 15 characters.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public char Status { get; set; }
        public char UserType { get; set; } = 'P';

        [Required(ErrorMessage = "Please select a chronic condition.")]
        public int SelectedChronicConditionId { get; set; }

        public List<ManageChronicConditionViewModel> AvailableChronicConditions { get; set; }

        public bool RegistrationSuccess { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public RegisterViewModel()
        {
            AvailableChronicConditions = GetChronicConditions();
        }





        //private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        
        public void RegisterPatient()
        {
            int patientId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertPatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@Surname", Surname);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@EmergencyPerson", EmergencyPerson);
                    command.Parameters.AddWithValue("@EmergencyContactNo", EmergencyContactNo);
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    SqlParameter patientIdParam = new SqlParameter("@PatientID", SqlDbType.Int);
                    patientIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(patientIdParam);

                    command.ExecuteNonQuery();
                    patientId = Convert.ToInt32(patientIdParam.Value);
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@ContactNo", ContactNo);
                    command.Parameters.AddWithValue("@UserType", UserType.ToString());
                    command.Parameters.AddWithValue("@Status", "A");
                    command.Parameters.AddWithValue("@IsDeleted", false);
                    command.Parameters.AddWithValue("@UserID", patientId);

                    command.ExecuteNonQuery();
                }
            }


            InsertSelectedChronicCondition(patientId, SelectedChronicConditionId);

            SendWelcomeEmail(Email);
        }

        private List<ManageChronicConditionViewModel> GetChronicConditions()
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


        private void InsertSelectedChronicCondition(int patientId, int selectedChronicConditionId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO PATIENT_CONDITION (PatientID, ConditionID, IsDeleted) VALUES (@PatientID, @ConditionID, @IsDeleted)", connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patientId);
                    command.Parameters.AddWithValue("@ConditionID", selectedChronicConditionId);
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    command.ExecuteNonQuery();
                }
            }
        }



        private void SendWelcomeEmail(string patientEmail)
        {
            string smtpServer = "smtp.gmail.com";
            string smtpUsername = "helpinghandsngo.team@gmail.com"; 
            string smtpPassword = "Team(12345)"; 
            int smtpPort = 587;

            SmtpClient client = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true,
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(smtpUsername), 
                Subject = "Welcome to Helping Hands", 
                Body = "Dear Patient,<br /><br />" +
                       "Welcome to Helping Hands! We're thrilled to have you as part of our community.<br /><br />" +
                       "Thank you for registering with us. Your account has been created successfully.<br /><br />" +
                       "If you have any questions or need assistance, feel free to contact us at help@helpinghands.com.<br /><br />" +
                       "Best regards,<br />Helping Hands Team", 
                IsBodyHtml = true,
            };

            mail.To.Add(patientEmail);

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error sending welcome email: " + ex.Message);
            }
        }


    }
}
