using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.Doctor
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
        public byte[] Photo { get; set; }

        public DoctorViewModel()
        {

        }
    }
}

