using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using HelpingHandsWeb.Models.ViewModels.AdminViewModels;
using System.ComponentModel.DataAnnotations;

namespace HelpingHandsWeb.Models.ViewModels.PatientViewModels
{
    public class PatientAppointmentsViewModel
    {
        [Key]
        public int AppointmentID { get; set; }

        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Title { get; set; }

        public TimeSpan Time { get; set; }

        public string MeetingType { get; set; }

        public TimeSpan MeetingTime { get; set; }

        public int Duration { get; set; }

        public string NurseName { get; set; }
    }

}
