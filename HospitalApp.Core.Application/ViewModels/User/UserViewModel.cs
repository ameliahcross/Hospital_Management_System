using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.User
{
	public class UserViewModel
	{
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}

