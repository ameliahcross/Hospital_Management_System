using System;
using HospitalApp.Core.Application.ViewModels.Patient;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IPatientSevice : IGenericService<PatientViewModel, SavePatientViewModel>
    {

    }
}

