using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelpingHandsWeb.Controllers;


namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientIndexViewModel
    {
        private readonly string _connectionString;
        private readonly BaseController _baseController;

        public string UserName { get; set; }
        public int PatientId { get; set; }
        public int TotalCareVisits { get; set; }
        public int TotalCareContracts { get; set; }

        public List<PatientConditionsViewModel> PatientConditions { get; set; }
        public IEnumerable<PatientAppointmentsViewModel> Appointments { get; set; }

        public PatientIndexViewModel(string userName, IConfiguration configuration, BaseController baseController)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            UserName = userName;
            _baseController = baseController;

            TotalCareVisits = _baseController.GetTotalCareVisits(_baseController.GetUserId(userName));
            TotalCareContracts = _baseController.GetTotalCareContracts(_baseController.GetUserId(userName));


            // You may need to adjust these lines based on your actual method signatures and return types
            PatientConditions = _baseController.GetPatientConditions(_baseController.GetUserId(userName)).ToList();
            Appointments = _baseController.GetPatientAppointments(_baseController.GetUserId(userName)).ToList();
        }

        // Your other methods remain unchanged
    }
}
