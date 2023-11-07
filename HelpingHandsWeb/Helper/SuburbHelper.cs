using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;

namespace HelpingHandsWeb.Helper
{
    public static class SuburbHelper
    {
        private static readonly IConfiguration _configuration;

        static SuburbHelper()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static List<SuburbViewModel> GetSuburbsFromDatabase(string suburb, string city, int recordCount, bool loadMore)
        {
            var suburbs = new List<SuburbViewModel>();

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (string.IsNullOrEmpty(suburb) && string.IsNullOrEmpty(city))
                {
                    var results = connection.Query<SuburbViewModel>("GetSuburb", commandType: CommandType.StoredProcedure);
                    suburbs = results.ToList();
                }
                else
                {
                    var results = connection.Query<SuburbViewModel>("SearchSuburbs",
                        new { Suburb = suburb, City = city },
                        commandType: CommandType.StoredProcedure);

                    suburbs = results.ToList();
                }

                if (!loadMore)
                {
                    suburbs = suburbs.Take(recordCount).ToList();
                }
            }

            return suburbs;
        }
    }
}
