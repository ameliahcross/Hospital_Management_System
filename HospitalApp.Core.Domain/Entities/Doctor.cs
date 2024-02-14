using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
	public class Doctor : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public byte[] Photo { get; set; }

        // relationships
        public ICollection<Appointment> Appointments { get; set; }
    }
}

