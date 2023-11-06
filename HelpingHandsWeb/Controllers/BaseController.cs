using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System;

namespace HelpingHandsWeb.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _configuration;

       
        protected readonly ApplicationDbContext _context; 

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
            var userName = ControllerContext.HttpContext?.Session?.Keys.Contains("UserName") == true
                ? ControllerContext.HttpContext.Session.GetString("UserName")
                : null;

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
