using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
	public class Patient : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergies { get; set; }
        public byte[] Photo { get; set; }

        // foreign keys
        public int PatientId { get; set; }

        // relationships
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<LabResult> LabResults { get; set; }
    }
}

