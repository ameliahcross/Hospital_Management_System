using System;
namespace HospitalApp.Core.Application.ViewModels.Patient
{
	public class PatientViewModel
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergies { get; set; }
        public string Photo { get; set; }

        public PatientViewModel()
		{
		}
	}
}

