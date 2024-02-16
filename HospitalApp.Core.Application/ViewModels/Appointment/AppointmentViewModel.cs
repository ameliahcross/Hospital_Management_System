using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Application.ViewModels.Doctor;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public IEnumerable<DoctorViewModel> Doctor { get; set; }
        public IEnumerable<PatientViewModel> Patient { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public AppointmentViewModel()
        {
        }
    }
}

