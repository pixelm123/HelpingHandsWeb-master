﻿using System;
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
        

     [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Surname")]
    public string Surname { get; set; }

    [Required]
    [Display(Name = "Gender")]
    public string Gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Emergency Contact Person")]
    public string EmergencyPerson { get; set; }

    [Required]
    [Display(Name = "Emergency Contact Number")]
    public string EmergencyContactNo { get; set; }

    [Required]
    [Display(Name = "Contact Number")]
    public string ContactNo { get; set; }

      


        [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    public bool RegistrationSuccess { get; set; }
        [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }


    }
}
