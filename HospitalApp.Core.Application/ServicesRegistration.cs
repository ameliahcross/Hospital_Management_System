using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalApp.Core.Application
{
    public static class ServicesRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
            #region "Services"
            services.AddTransient<IAppointmentService, AppointmentService>();
            #endregion
        }

    }
}

 