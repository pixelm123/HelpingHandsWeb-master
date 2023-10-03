using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string connectionString = "SICT-SQL.MANDELA.AC.ZA;Database=GRP-04-34-HelpingHandsDB;User ID=GRP-04-34;Password=grp-04-34-2023#;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IActionResult UserProfile()
        {
            string userName = HttpContext.Session.GetString("UserName"); 
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Login"); 
            }

            
            var userModel = GetUserByUsername(userName);

            
            var model = new UserProfileViewModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                ContactNumber = userModel.ContactNo,
                UserType = userModel.UserType 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                UpdateUserProfile(model);

               
                return RedirectToAction("UserProfile"); 
            }

            return View(model);
        }

        private User GetUserByUsername(string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[USER] WHERE [UserName] = @UserName", connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                ContactNo = reader["ContactNo"].ToString(),
                                UserType = reader["UserType"].ToString()
                            };
                        }
                    }
                }
            }

            return null; 
        }

        private void UpdateUserProfile(UserProfileViewModel model)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE [dbo].[USER] SET [Email] = @Email, [ContactNo] = @ContactNo WHERE [UserName] = @UserName", connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@ContactNo", model.ContactNumber);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
