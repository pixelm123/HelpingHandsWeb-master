using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Data;

using System.Data.SqlClient;
using System;

namespace HelpingHandsWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _configuration;

       
        protected readonly ApplicationDbContext _context;  // Add this line

        public BaseController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            SetUserNameInViewData();
        }



        protected string GetUserDisplayName()
        {
            var userName = ControllerContext.HttpContext?.Session.GetString("UserName");
            return userName;
        }



        private string SetUserNameInViewData()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;
            return userName;
        }


        protected string ConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("DefaultConnection");
            }
        }
    }

}
