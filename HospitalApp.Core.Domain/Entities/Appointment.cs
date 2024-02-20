using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public enum AppointmentStatus
    {
        Consulta_Pendiente,
        Pendiente_Resultados,
        Completada
    }

    public class Appointment : BaseEntity
	{
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Reason { get; set; }
        public AppointmentStatus Status { get; set; }

        // foreign keys
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        // relationships
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

        // navigation property
        public ICollection<LabResult> LabResults { get; set; }


    }
}

