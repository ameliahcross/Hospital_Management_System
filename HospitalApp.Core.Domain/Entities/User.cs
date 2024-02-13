using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
	public class User : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; } //almacenar un hash de la contraseña, no la contraseña en texto plano
        public string UserType { get; set; } 
    }
}

