using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels
{
    
        public class LoginViewModel
        {


        [Required(ErrorMessage = "Please enter your username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserDisplayName { get; set; }


         }
    

}
