using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public enum UserType
    {
       Admin,
       Assistant
    }

    public class User : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; } 
        public UserType UserType { get; set; } 
    }
}

