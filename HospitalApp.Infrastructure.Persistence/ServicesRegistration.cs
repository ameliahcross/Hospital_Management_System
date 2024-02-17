using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Infrastructure.Persistence.Contexts;
using HospitalApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalApp.Infrastructure.Persistence
{
	public static class ServicesRegistration
	{
		public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
		{
            #region "Contexts configuration"

            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(option => option.UseInMemoryDatabase("InMemoryDb")); 
            }
            else
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                    migrations => migrations.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
         
            #endregion

            #region "Repositories"
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            #endregion
        }

    }
}

 