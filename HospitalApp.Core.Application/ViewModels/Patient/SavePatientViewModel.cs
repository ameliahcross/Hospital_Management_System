using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.Patient
{
	public class SavePatientViewModel
	{
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de teléfono")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El teléfono solo puede contener números")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Debe ingresar una dirección")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cédula")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "La cédula solo puede contener números")]
        public string IdentificationNumber { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public bool IsSmoker { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public bool HasAllergies { get; set; }

        [Required(ErrorMessage = "Debe proporcionar una foto")]
        public byte[] Photo { get; set; }

        public SavePatientViewModel()
		{
		}
	}
}

