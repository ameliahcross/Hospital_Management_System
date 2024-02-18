using System;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels;
using HospitalApp.Core.Application.ViewModels.Appointment;
using HospitalApp.Core.Application.ViewModels.LabTest;

namespace HospitalApp.Core.Application.Interfaces.Repositories
{
	public interface ILabTestService : IGenericService<LabTestViewModel, SaveLabTestViewModel>
    {

    }
}

