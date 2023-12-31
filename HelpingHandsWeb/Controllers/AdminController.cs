﻿using HelpingHandsWeb.Data;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using HelpingHandsWeb.Models;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using HelpingHandsWeb.Models.ViewModels.PatientViewModels;
using HelpingHandsWeb.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using Microsoft.Extensions.Configuration;
using HelpingHandsWeb.Models.Users;
using System.Collections.Generic;
using HelpingHandsWeb.Helper;

namespace HelpingHandsWeb.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(ApplicationDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }


        [HttpGet("AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            var userDisplayName = GetUserDisplayName();
            var viewModel = new AdminIndexViewModel(userDisplayName, _configuration);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                viewModel.TotalOfficeManagers = connection.QueryFirstOrDefault<int>("GetTotalOfficeManagers", commandType: CommandType.StoredProcedure);
                viewModel.TotalPatients = connection.QueryFirstOrDefault<int>("GetTotalPatients", commandType: CommandType.StoredProcedure);
                viewModel.TotalNurses = connection.QueryFirstOrDefault<int>("GetTotalNurses", commandType: CommandType.StoredProcedure);
                viewModel.TotalChronicConditions = connection.QueryFirstOrDefault<int>("GetTotalChronicConditions", commandType: CommandType.StoredProcedure);

                var patientResults = connection.Query<PatientViewModel>("GetPatients", commandType: CommandType.StoredProcedure);
                viewModel.Patients = patientResults.ToList();

                var nurseResults = connection.Query<NurseViewModel>("GetNurses", commandType: CommandType.StoredProcedure);
                viewModel.Nurses = nurseResults.ToList();
            }

       
            ViewData["UserName"] = userDisplayName;

            return View("AdminDashboard", viewModel);
        }

        [HttpPost("AdminDashboard")]
        public IActionResult AdminDashboard(AdminIndexViewModel model)
        {


            return View("AdminDashboard", model);
        }

        [HttpPost("change-password")]
        public IActionResult cc(AdminIndexViewModel model)
        {
            return View("change-password", model);
        }

        [HttpPost("profile")]
        public IActionResult Profile(AdminProfileViewModel model)
        {
            // Implementation
            return View("profile", model);
        }

        [HttpGet("cities")]
        public IActionResult Cities()
        {
            var model = new List<CityViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<CityViewModel>("GetCities", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("cities", model);
        }

        [HttpGet("add-city")]
        public IActionResult AddCity()
        {
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-city", new CityViewModel());
        }

        [HttpPost("add-city")]
        public IActionResult AddCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertCity", new
                    {
                        Name = model.Name,
                        Abbreviation = model.Abbreviation
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Cities");
            }

            ViewData["UserName"] = GetUserDisplayName();
            return View("add-city", model);
        }

        [HttpPost("edit-city")]
        public IActionResult EditCity(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateCity", new
                    {
                        CityId = model.CityId,
                        Name = model.Name,
                        Abbreviation = model.Abbreviation
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Cities");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-city", model);
        }

        [HttpPost("delete-city/{id}")]
        public IActionResult DeleteCity(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteCity", new
                {
                    CityId = id
                }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Cities");
        }


        [HttpPost("add-suburb")]
        public IActionResult AddSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertSuburb", new
                    {
                        Suburb = model.Suburb,
                        PostalCode = model.PostalCode,
                        CityId = model.CityId
                    }, commandType: CommandType.StoredProcedure);
                }
                ViewData["UserName"] = GetUserDisplayName();
                return RedirectToAction("Suburbs");
            }
            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);
            return View("add-suburb", model);
        }

        [HttpPost("edit-suburb")]
        public IActionResult EditSuburb(SuburbViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateSuburb", new
                    {
                        SuburbID = model.SuburbID,
                        Suburb = model.Suburb,
                        PostalCode = model.PostalCode,
                        CityId = model.CityId
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Suburbs");
            }
            ViewBag.CityList = new SelectList(_context.Cities.Where(c => !c.IsDeleted), "CityId", "Name", model.CityId);
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-suburb", model);
        }

        [HttpPost("delete-suburb/{id}")]
        public IActionResult DeleteSuburb(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteSuburb", new
                {
                    SuburbID = id
                }, commandType: CommandType.StoredProcedure);
            }
            return RedirectToAction("Suburbs");
        }
        [HttpGet("Admin/suburbs")]
        [HttpPost("Admin/suburbs")]
        public IActionResult Suburbs(string suburb, string city, int recordCount, bool loadMore)
        {
            var model = SuburbHelper.GetSuburbsFromDatabase(suburb, city, recordCount, loadMore);
            ViewData["UserName"] = GetUserDisplayName();
            return View("suburbs", model);
        }



        [HttpGet("conditions")]
        public IActionResult Conditions()
        {
            var model = new List<ConditionViewModel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var results = connection.Query<ConditionViewModel>("GetChronicCondition", commandType: CommandType.StoredProcedure);
                model = results.ToList();
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("conditions", model);
        }

        [HttpGet("add-condition")]
        public IActionResult AddCondition()
        {
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-condition", new ConditionViewModel());
        }

        [HttpPost("add-condition")]
        public IActionResult AddCondition(ConditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertChronicCondition", new
                    {
                        Name = model.Name,
                        Description = model.Description
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Conditions");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-condition", model);
        }
        [HttpGet("edit-condition/{id}")]
        public IActionResult EditCondition(int id)
        {
            var model = new ConditionViewModel();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<ConditionViewModel>(
                    "GetCondition",
                    new { ConditionID = id },
                    commandType: CommandType.StoredProcedure
                );
                if (result == null)
                {
                    return RedirectToAction("Conditions");
                }
                model = result;
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-condition", model);
        }

        [HttpPost("edit-condition")]
        public IActionResult EditCondition(ConditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateChronicCondition", new
                    {
                        ConditionID = model.ConditionID,
                        Name = model.Name,
                        Description = model.Description
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Conditions");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-condition", model);
        }

        [HttpPost("delete-condition/{id}")]
        public IActionResult DeleteCondition(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteChronicCondition", new { ConditionID = id }, commandType: CommandType.StoredProcedure);
            }
            ViewData["UserName"] = GetUserDisplayName();
            return RedirectToAction("Conditions");
        }

        [HttpGet("add-officemanager")]
        public IActionResult AddOfficeManager()
        {
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-officemanager", new List<OfficeManagerViewModel> { new OfficeManagerViewModel() });
        }

        [HttpPost("add-officemanager")]
        public IActionResult AddOfficeManager(OfficeManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertUser", new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "O",
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("OfficeManagers");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-officemanager", new List<OfficeManagerViewModel> { model });
        }

        [HttpGet("edit-officemanager/{id}")]
        public IActionResult EditOfficeManager(int id)
        {
            var model = new OfficeManagerViewModel();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<OfficeManagerViewModel>("GetUser", new { UserID = id }, commandType: CommandType.StoredProcedure);
                if (result == null)
                {
                    return RedirectToAction("OfficeManagers");
                }
                model = result;
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-officemanager", new List<OfficeManagerViewModel> { model });
        }

        [HttpPost("edit-officemanager")]
        public IActionResult EditOfficeManager(OfficeManagerViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateUser", new
                    {
                        UserID = model.UserID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "O",
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("OfficeManagers");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-officemanager", new List<OfficeManagerViewModel> { model });
        }

        [HttpGet("officemanagers")]
        public IActionResult OfficeManagers(string username)
        {
            var model = new List<OfficeManagerViewModel>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (string.IsNullOrEmpty(username))
                {
                    var results = connection.Query<OfficeManagerViewModel>("GetUser", commandType: CommandType.StoredProcedure);
                    model = results.Where(o => o.UserType == "O").ToList();
                }
                else
                {
                    var searchResults = connection.Query<OfficeManagerViewModel>("SearchOfficeManagers", new { Username = username }, commandType: CommandType.StoredProcedure);
                    model = searchResults.ToList();
                }
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("officemanagers", model);
        }
        [HttpGet("add-nurse")]
        public IActionResult AddNurse()
        {
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-nurse", new List<NurseViewModel> { new NurseViewModel() });
        }

        [HttpPost("add-nurse")]
        public IActionResult AddNurse(NurseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("InsertNurse", new
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        UserType = "N",
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        IDNo = model.IDNo
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Nurses");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("add-nurse", new List<NurseViewModel> { model });
        }


        [HttpPost("edit-nurse")]
        public IActionResult EditNurse(NurseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute("UpdateNurse", new
                    {
                        UserID = model.UserID,
                        UserName = model.UserName,
                        Password = model.Password,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        Status = model.Status,
                        ProfilePicture = model.ProfilePicture ?? (object)DBNull.Value,
                        FirstName = model.FirstName,
                        Surname = model.Surname,
                        Gender = model.Gender,
                        IDNo = model.IDNo
                    }, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Nurses");
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("edit-nurse", model);
        }

        [HttpPost("delete-nurse")]
        public IActionResult DeleteNurse(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("DeleteNurse", new
                {
                    UserID = userId
                }, commandType: CommandType.StoredProcedure);
            }
            ViewData["UserName"] = GetUserDisplayName();
            return RedirectToAction("Nurses");
        }

        [HttpGet("Admin/nurses")]
        [HttpPost("Admin/nurses")]
        public IActionResult Nurses(string firstName, string surname, string gender)
        {
            var model = new List<NurseViewModel>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(surname) && string.IsNullOrEmpty(gender))
                {
                    var results = connection.Query<NurseViewModel>("GetNurses", commandType: CommandType.StoredProcedure);
                    model = results.ToList();
                }
                else
                {
                    if (gender == "Male")
                    {
                        gender = "M";
                    }
                    else if (gender == "Female")
                    {
                        gender = "F";
                    }

                    var results = connection.Query<NurseViewModel>("SearchNurses",
                        new { FirstName = firstName, Surname = surname, Gender = gender },
                        commandType: CommandType.StoredProcedure);

                    model = results.ToList();
                }
            }

            ViewData["UserName"] = GetUserDisplayName();
            return View("nurses", model);

        }

        [HttpGet("Admin/patients")]
        [HttpPost("Admin/patients")]
        public IActionResult Patients(string firstName, string surname, string gender)
        {
            var model = new List<PatientViewModel>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(surname) && string.IsNullOrEmpty(gender))
                {
                    var results = connection.Query<PatientViewModel>("GetPatients", commandType: CommandType.StoredProcedure);
                    model = results.ToList();
                }
                else
                {
                    if (gender == "Male")
                    {
                        gender = "M";
                    }
                    else if (gender == "Female")
                    {
                        gender = "F";
                    }

                    var results = connection.Query<PatientViewModel>("SearchPatients",
                        new { FirstName = firstName, Surname = surname, Gender = gender },
                        commandType: CommandType.StoredProcedure);

                    model = results.ToList();
                }
            }
            ViewData["UserName"] = GetUserDisplayName();
            return View("patients", model);
        }
    }
}