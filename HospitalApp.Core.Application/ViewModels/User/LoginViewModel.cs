﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
	}
}

