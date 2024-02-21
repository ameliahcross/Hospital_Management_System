using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public int? LabResultId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }

        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public bool IsPendingConsultation { get; set; }
        public bool IsPendingResults { get; set; }
        public bool IsCompleted { get; set; }
    }
}

