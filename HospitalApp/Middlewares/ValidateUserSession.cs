using System;
using HospitalApp.Core.Application.Helpers;
using HospitalApp.Core.Application.ViewModels.User;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Middlewares
{
	public class ValidateUserSession
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public bool HasUser()
		{
			// "userViewModel" buscará la sesión en el key "user" a ver si existe algún usuario creado de tipo
			// "UserViewModel".Esto hay que inyectarlo para tenerlo accesible desde cualquier parte del app.

			UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

			if (userViewModel == null)
			{
				return false;
			}
			return true;
			// Con llamar aun méthod de esta clase "userViewModel" sabré si hay un usuario loggueado o no.
		}

        public UserRole? GetUserRole()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            return userViewModel?.Role;
        }
    }
}

