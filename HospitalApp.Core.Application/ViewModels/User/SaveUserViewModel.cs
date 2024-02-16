using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class SaveUserViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare("Password", ErrorMessage = "No coinciden las contraseñas ingresadas")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe seleccionar el tipo de usuario")]
        public UserType UserType { get; set; }

        public SaveUserViewModel()
		{
		}
	}
}

