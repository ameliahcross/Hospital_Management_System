using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class SaveUserViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare(nameof(Password), ErrorMessage = "No coinciden las contraseñas ingresadas")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe ingresar un correo")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar el apellido")]
        public string LastName { get; set; }

       
	}
}

