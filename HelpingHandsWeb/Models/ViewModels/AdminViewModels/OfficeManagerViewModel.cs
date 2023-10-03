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
    public class OfficeManagerViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public char Status { get; set; }

        public string UserDisplayName { get; set; }
    }


}
