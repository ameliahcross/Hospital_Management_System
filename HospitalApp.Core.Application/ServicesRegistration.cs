using HospitalApp.Core.Application.Interfaces.Repositories;
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
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<ILabResultService, LabResultService>();
            services.AddTransient<ILabTestService, LabTestService>();
            services.AddTransient<IPatientSevice, PatientService>();
            services.AddTransient<IUserService, UserService>();
            #endregion            
        }

    }
}

 