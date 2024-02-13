using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public enum AppointmentStatus
    {
        PendingAppointment,
        PendingResults,
        Completed
    }

    public class Appointment : BaseEntity
	{
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }

        // Relaciones
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}

