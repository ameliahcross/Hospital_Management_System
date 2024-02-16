using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.Doctor
{
    public class SaveDoctorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de teléfono")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono solo puede contener números")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cédula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "La cédula solo puede contener números")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe proporcionar una foto")]
        public byte[] Photo { get; set; }

        public SaveDoctorViewModel()
        {

        }
    }
}

