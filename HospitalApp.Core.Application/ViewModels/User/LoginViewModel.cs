using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
       
	}
}

