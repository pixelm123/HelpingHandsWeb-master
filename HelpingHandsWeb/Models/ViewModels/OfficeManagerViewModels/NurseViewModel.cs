using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels
{
    public class NurseViewModel
    {

        public int NurseId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Select Gender .")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "ID Number is required.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "ID Number must be a 13-digit number.")]
        public string IDNo { get; set; }



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
        public char UserType { get; set; }



        [Required(ErrorMessage = "Temporary password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string TemporaryPassword { get; set; }

        private readonly string connectionString = "Server=SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;MultipleActiveResultSets=True;";

        public void RegisterNurse()
        {
            string temporaryPassword = GenerateRandomPassword();

            int nurseId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertNurse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@Surname", Surname);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@IDNo", IDNo);
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    SqlParameter nurseIdParam = new SqlParameter("@NurseId", SqlDbType.Int);
                    nurseIdParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(nurseIdParam);

                    command.ExecuteNonQuery();
                    nurseId = Convert.ToInt32(nurseIdParam.Value);
                }
            }

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
                    command.Parameters.AddWithValue("@UserType", "N");
                    command.Parameters.AddWithValue("@Status", "A"); 
                    command.Parameters.AddWithValue("@IsDeleted", false);
                    command.Parameters.AddWithValue("@UserID", nurseId);

                    command.ExecuteNonQuery();
                }
            }

            SendWelcomeEmail(Email, UserName, temporaryPassword);
        }

        private void SendWelcomeEmail(string nurseEmail, string nurseUsername, string temporaryPassword)
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
                Subject = "Nurse Registration Confirmation",
                Body = $"Dear Nurse {nurseUsername},<br /><br />" +
                       "Welcome to Helping Hands! Your account has been created successfully.<br /><br />" +
                       $"Your username: {nurseUsername}<br />" +
                       $"Temporary password: {temporaryPassword}<br /><br />" +
                       "Please log in and update your password as soon as possible.<br /><br />" +
                       "If you have any questions or need assistance, feel free to contact us at help@helpinghands.com.<br /><br />" +
                       "Best regards,<br />Helping Hands Team",
                IsBodyHtml = true,
            };

            mail.To.Add(nurseEmail);

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending welcome email: " + ex.Message);
            }
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



        public void UpdateNurse(int nurseId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateNurse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NurseId", nurseId);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@Surname", Surname);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@IDNo", IDNo);

                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand("UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", nurseId);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@ContactNo", ContactNo);

                    command.ExecuteNonQuery();
                }
            }
        }



        public void GetNurse()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAllNurses", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FirstName = reader["FirstName"].ToString();
                            Surname = reader["Surname"].ToString();
                            Gender = reader["Gender"].ToString();
                            IDNo = reader["IDNo"].ToString();
                            UserName = reader["UserName"].ToString();
                            Email = reader["Email"].ToString();
                            ContactNo = reader["ContactNo"].ToString();
                        }
                    }
                }
            }
        }


        public void DeleteNurse(int nurseId)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DeleteNurse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NurseId", nurseId);

                    command.ExecuteNonQuery();
                }

                using (SqlCommand command = new SqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", nurseId);
                    command.Parameters.AddWithValue("@IsDeleted", true);

                    command.ExecuteNonQuery();
                }
            }
        }

    }


}
