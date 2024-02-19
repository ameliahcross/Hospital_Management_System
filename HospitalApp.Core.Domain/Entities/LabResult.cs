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

        // relationships
        //public Patient Appointment { get; set; }
        public LabTest LabTest { get; set; }

        // foreing keys
        public int LabTestId { get; set; }
        public int AppointmentId { get; set; } // nuevo FK para crear resultados

        // navigation property
        public Appointment Appointment { get; set; }

    }
}

