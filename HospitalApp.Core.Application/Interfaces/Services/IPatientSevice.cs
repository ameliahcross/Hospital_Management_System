using System;
using HospitalApp.Core.Application.ViewModels.Patient;
using System.Threading.Tasks;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IPatientSevice
	{
        Task<List<PatientViewModel>> GetAllViewModel();
        Task<PatientViewModel> GetByIdSaveViewModel(int id);
        Task Update(PatientViewModel patientToSave);
        Task Add(PatientViewModel patientToCreate);
        Task Delete(int id);
    }
}

