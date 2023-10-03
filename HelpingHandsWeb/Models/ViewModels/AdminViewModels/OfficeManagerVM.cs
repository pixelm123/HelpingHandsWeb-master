using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class OfficeManagerVM
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

        public char Status { get; set; }
        public char UserType { get; set; } = 'O';

        [Required(ErrorMessage = "Temporary password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string TemporaryPassword { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public void RegisterOfficeManager()
        {
            string temporaryPassword = GenerateRandomPassword();

            int officeManagerId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", HashPassword(temporaryPassword));
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@ContactNo", ContactNo);
                    command.Parameters.AddWithValue("@UserType", "O");
                    command.Parameters.AddWithValue("@Status", "I");
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    SqlParameter officeManagerIdParam = new SqlParameter("@UserID", SqlDbType.Int);
                    officeManagerIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(officeManagerIdParam);

                    command.ExecuteNonQuery();
                    officeManagerId = Convert.ToInt32(officeManagerIdParam.Value);
                }
            }

            SendWelcomeEmail(Email, UserName, temporaryPassword);
            ;
        }


        private string GenerateRandomPassword()
        {
            int passwordLength = 12;
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[passwordLength];
                rng.GetBytes(randomBytes);

                StringBuilder sb = new StringBuilder(passwordLength);
                foreach (byte b in randomBytes)
                {
                    sb.Append(validChars[b % validChars.Length]);
                }

                return sb.ToString();
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        private void SendWelcomeEmail(string officeManagerEmail, string officeManagerUsername, string temporaryPassword)
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
                From = new MailAddress("helpinghandsngo.team@gmail.com"),
                Subject = "Office Manager Registration Confirmation",
                Body = $"Dear Office Manager {officeManagerUsername},<br /><br />" +
                       "Welcome to Helping Hands! Your account has been created successfully.<br /><br />" +
                       $"Your username: {officeManagerUsername}<br />" +
                       $"Temporary password: {temporaryPassword}<br /><br />" +
                       "Please log in and update your password as soon as possible.<br /><br />" +
                       "If you have any questions or need assistance, feel free to contact us at help@helpinghands.com.<br /><br />" +
                       "Best regards,<br />Helping Hands Team",
                IsBodyHtml = true,
            };

            mail.To.Add(officeManagerEmail);

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending welcome email: " + ex.Message);
            }
        }


        //public void GetOfficeManager(int officeManagerId)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("GetUser", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", officeManagerId);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    UserName = reader["UserName"].ToString();
        //                    Email = reader["Email"].ToString();
        //                    ContactNo = reader["ContactNo"].ToString();
        //                }
        //            }
        //        }
        //    }
        //}

        public void DeleteOfficeManager(int officeManagerId)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", officeManagerId);
                    command.Parameters.AddWithValue("@IsDeleted", true);

                    command.ExecuteNonQuery();
                }
            }
        }



    }
}
