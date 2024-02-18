using System;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.Doctor;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IDoctorService : IGenericService<DoctorViewModel, SaveDoctorViewModel>
    {

    }
}

