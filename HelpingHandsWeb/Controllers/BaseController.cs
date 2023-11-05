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
        protected readonly ApplicationDbContext _context;
        protected readonly IConfiguration _configuration;

        public BaseController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        protected string GetUserDisplayName()
        {
            return HttpContext.Session.GetString("UserName");
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
