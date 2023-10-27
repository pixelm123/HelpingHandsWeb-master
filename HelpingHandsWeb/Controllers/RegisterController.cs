using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Models.ViewModels.OfficeManagerViewModels;
using HelpingHandsWeb.Models.ViewModels.NurseViewModels;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using HelpingHandsWeb.Models.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace HelpingHandsWeb.Controllers
{
    public class RegisterController : Controller
    {
       private readonly IConfiguration _configuration;

         public RegisterController(IConfiguration configuration)
          {
            _configuration = configuration;
          }

       private string ConnectionString
        {
           get
           {
               return _configuration.GetConnectionString("DefaultConnection");
           }
        }

         
        public IActionResult Register()
        {
            var model = new RegisterViewModel(); 
            return View("~/Views/Home/register.cshtml", model);
        }
         [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

               
                connection.Execute(
                    "InsertPatient",
                    new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "P", 
                        Status = "A",
                        ProfilePicture = (object)DBNull.Value,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        DateOfBirth = model.DateOfBirth,
                        EmergencyPerson = model.EmergencyPerson,
                        EmergencyContactNo = model.EmergencyContactNo
                    },
                    commandType: CommandType.StoredProcedure
                );

                
            }

            return RedirectToAction("Login", "Home");
        }

        
        return View("~/Views/Register.cshtml", model);
    }



    }
}
