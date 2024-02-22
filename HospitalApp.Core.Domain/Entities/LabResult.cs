using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public enum LabResultStatus
    {
        Pendiente,
        Completado
    }

    public class LabResult : BaseEntity
    {
        public string Result { get; set; }
        public LabResultStatus Status { get; set; }

        // Relationship
        public LabTest LabTest { get; set; }

        // ForeignKey
        public int LabTestId { get; set; }
        public int AppointmentId { get; set; }

        // Navigation property
        public Appointment Appointment { get; set; }
    }

}

