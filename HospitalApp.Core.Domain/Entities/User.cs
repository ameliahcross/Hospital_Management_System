using System;
using HospitalApp.Core.Domain.Common;

namespace HospitalApp.Core.Domain.Entities
{
    public class User : BaseEntity
	{
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
         
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}

