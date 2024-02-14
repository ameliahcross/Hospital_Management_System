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

        // foreign keys
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        // relationships
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}

