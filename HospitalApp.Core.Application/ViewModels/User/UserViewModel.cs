using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class UserViewModel
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserType UserType { get; set; }

        public UserViewModel()
		{
		}
	}
}

