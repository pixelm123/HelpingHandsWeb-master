using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelpingHandsWeb.Models.Users;

namespace HelpingHandsWeb.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int NurseID { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date and time format.")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "Meeting Type is required.")]
        [StringLength(50, ErrorMessage = "Meeting Type cannot exceed 50 characters.")]
        public string MeetingType { get; set; }

        [Required(ErrorMessage = "Meeting Time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        public TimeSpan MeetingTime { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        public int Duration { get; set; }

        
        public Patient Patient { get; set; }

        public Nurse Nurse { get; set; }

    }
}
