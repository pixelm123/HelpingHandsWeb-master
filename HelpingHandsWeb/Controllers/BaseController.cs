using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System;
using Humanizer.Configuration;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using System.Data;
using HelpingHandsWeb.Models.Users;

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


        public string GetUserDisplayName()
        {
            var userName = ControllerContext.HttpContext?.Session.GetString("UserName");
            return userName;
        }


        public string SetUserNameInViewData()
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

        public int GetUserId(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetUserIdByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", userName);

                    try
                    {
                        return (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred in GetUserId: {ex.Message}");
                        return 0;
                    }
                }
            }
        }

        public Patient GetPatientById(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);


                var patient = connection.QueryFirstOrDefault<Patient>(
                    @"SELECT * FROM PATIENTS WHERE UserID = @userId;", parameters);

                return patient;
            }
        }

        public User GetUserById(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                var user = connection.QueryFirstOrDefault<User>(
                    @"SELECT * FROM [USER] WHERE UserID = @userId;", parameters);

                return user;
            }
        }


        public int GetTotalCareContracts(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                // Fetch the total number of care contracts for the patient
                var result = connection.QueryFirstOrDefault<int>(
                    @"SELECT COUNT(ContractID)
                  FROM CARE_CONTRACT
                  WHERE PatientId = (SELECT PatientId FROM PATIENTS WHERE UserID = @userId) AND IsDeleted = 0;"
                    , parameters
                );

                return result;
            }
        }

        public int GetTotalCareVisits(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                // Fetch the total number of care visits for the patient
                var result = connection.QueryFirstOrDefault<int>(
                    @"SELECT COUNT(VisitID)
                  FROM CARE_VISIT
                  WHERE PatientId = (SELECT PatientId FROM PATIENTS WHERE UserID = @userId) AND IsDeleted = 0;"
                    , parameters
                );

                return result;
            }
        }
        public IEnumerable<PatientCareContractsViewModel> GetCareContractsForPatient(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                // Fetch patient's care contracts
                var results = connection.Query<PatientCareContractsViewModel>(
                    @"SELECT CC.ContractID,
                 CC.ContractDate,
                 CC.AddressLine1,
                 CC.AddressLine2,
                 CC.WoundDescription,
                 CC.StartCareDate,
                 CC.EndCareDate,
                 CC.ContractStatus
                 FROM CARE_CONTRACT AS CC
                 WHERE CC.PatientId = (SELECT PatientId FROM PATIENTS WHERE UserID = @userId) AND CC.IsDeleted = 0;"
                    , parameters
                );

                return results;
            }
        }

        public IEnumerable<PatientAppointmentsViewModel> GetPatientAppointments(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                // Fetch patient's appointments
                var results = connection.Query<PatientAppointmentsViewModel>(
                    @"SELECT A.AppointmentID,
                 A.AppointmentDate,
                 A.Title,
                 A.Time,
            
                
                 A.Duration
                 FROM APPOINTMENT AS A
                 WHERE A.PatientId = (SELECT PatientId FROM PATIENTS WHERE UserID = @userId) AND A.IsDeleted = 0;"
                    , parameters
                );

                return results;
            }
        }

        public IEnumerable<PatientConditionsViewModel> GetPatientConditions(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);

                var results = connection.Query<PatientConditionsViewModel>(
                    @"SELECT PC.PatientId,
                 PC.ConditionID,
        
                 CC.Name,
                 CC.Description
              FROM PATIENT_CONDITION AS PC
              INNER JOIN CHRONIC_CONDITION AS CC ON PC.ConditionID = CC.ConditionID
              INNER JOIN PATIENTS AS P ON PC.PatientId = P.PatientId
              WHERE P.UserID = @userId;"
                    , parameters
                );

                return results;
            }
        }


        //public IEnumerable<PatientConditionsViewModel> GetPatientConditions(int userId)
        //{
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        connection.Open();

        //        var parameters = new DynamicParameters();
        //        parameters.Add("userId", userId);

        //        var results = connection.Query<PatientConditionsViewModel>(
        //            @"SELECT PC.PatientId,
        //             PC.ConditionID,
        //             PC.IsDeleted,
        //             CC.Name,
        //             CC.Description
        //      FROM PATIENT_CONDITION AS PC
        //      INNER JOIN CHRONIC_CONDITION AS CC ON PC.ConditionID = CC.ConditionID
        //      INNER JOIN PATIENTS AS P ON PC.PatientId = P.PatientId
        //      WHERE P.UserID = @userId;"
        //            , parameters
        //        );

        //        return results;
        //    }
        //}

        public void UpdatePassword(int userId, string newPassword)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId);
                parameters.Add("newPassword", newPassword);


                connection.Execute(
                    @"UPDATE [USER] SET Password = @newPassword WHERE UserID = @userId;",
                    parameters
                );
            }
        }
        public void UpdatePatientChronicConditions(int userId, List<int> selectedConditionIds)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();


                var deleteParameters = new DynamicParameters();
                deleteParameters.Add("userId", userId);
                connection.Execute(
                    @"DELETE FROM PATIENT_CONDITION WHERE PatientId = (SELECT PatientId FROM PATIENTS WHERE UserID = @userId);",
                    deleteParameters
                );


                foreach (var conditionId in selectedConditionIds)
                {
                    var insertParameters = new DynamicParameters();
                    insertParameters.Add("userId", userId);
                    insertParameters.Add("conditionId", conditionId);
                    connection.Execute(
                        @"INSERT INTO PATIENT_CONDITION (PatientId, ConditionID, IsDeleted) 
                  VALUES ((SELECT PatientId FROM PATIENTS WHERE UserID = @userId), @conditionId, 0);",
                        insertParameters
                    );
                }
            }
        }




    }

}
