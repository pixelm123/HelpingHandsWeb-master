using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HelpingHandsWeb.Models.ViewModels.AdminViewModels
{
    public class NurseViewModel
    {

     public int NurseID { get; set; }
    
    
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Contact number is required.")]
    public string ContactNo { get; set; }

    [Required(ErrorMessage = "User type is required.")]
    public char UserType { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public char Status { get; set; }

    [Required(ErrorMessage = "Profile picture is required.")]
    public byte[] ProfilePicture { get; set; }

    
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Surname is required.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Gender is required.")]
    public string Gender { get; set; }

    [Required(ErrorMessage = "ID number is required.")]
    public string IDNo { get; set; }

    }
}


