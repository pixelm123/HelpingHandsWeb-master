using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class AdminViewModel
    {
        public int UserID { get; set; }


        [Required(ErrorMessage = "User Name is required.")]
        [StringLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special characters are not allowed in the username.")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", ErrorMessage = "Email address does not match the expected format (e.g., name@example.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^(?=.*\d)[\d\s-]+$", ErrorMessage = "Phone number should be in the format: 071 234 5678.")]
        [StringLength(15, ErrorMessage = "Contact Number cannot exceed 10 characters.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        public char Status { get; set; }
        public char UserType { get; set; } = 'A';


        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public void RegisterAdmin()
        {
           

            int UserID;
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
                    command.Parameters.AddWithValue("@UserType", "O");
                    command.Parameters.AddWithValue("@Status", "I");
                    command.Parameters.AddWithValue("@IsDeleted", false);

                   

                    command.ExecuteNonQuery();
                   
                }
            }

            SendWelcomeEmail(Email);
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
