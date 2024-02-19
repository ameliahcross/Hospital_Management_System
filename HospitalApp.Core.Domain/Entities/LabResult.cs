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
        public Patient Patient { get; set; }
        public LabTest LabTest { get; set; }

        // foreing keys
        public int LabTestId { get; set; }
        public int PatientId { get; set; }

    }
}

