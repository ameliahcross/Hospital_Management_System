using System;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.Patient
{
	public class SavePatientViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de teléfono")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe contener 10 dígitos")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cédula")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula debe contener 11 dígitos")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida")]
        public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public bool IsSmoker { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public bool HasAllergies { get; set; }

        [Required(ErrorMessage = "Debe proporcionar una foto")]
        public string Photo { get; set; }

        //[DataType(DataType.Upload)]
        //public IFormFile File { get; set; }

        public SavePatientViewModel()
		{
		}
	}
}

