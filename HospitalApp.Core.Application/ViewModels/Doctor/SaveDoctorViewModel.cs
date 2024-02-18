using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener 10 dígitos")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cédula")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula debe contener 11 dígitos")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe proporcionar una foto")]
        public string Photo { get; set; }

        //[DataType(DataType.Upload)]
        //public IFormFile File { get; set; }

        public SaveDoctorViewModel()
        {

        }
    }
}

