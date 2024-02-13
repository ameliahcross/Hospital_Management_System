using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public enum LabResultStatus
    {
        Pending,
        Completed
    }

    public class LabResult : BaseEntity
	{
        public string Result { get; set; }
        public LabResultStatus Status { get; set; }

        // Relaciones
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int LabTestId { get; set; }
        public LabTest LabTest { get; set; }
    }
}

