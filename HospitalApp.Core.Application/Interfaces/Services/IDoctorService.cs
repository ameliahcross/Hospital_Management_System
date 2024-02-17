using System;
using HospitalApp.Core.Application.ViewModels.Doctor;

namespace HospitalApp.Core.Application.Interfaces.Services
{
	public interface IDoctorService
	{
        Task<List<DoctorViewModel>> GetAllViewModel();
        Task<DoctorViewModel> GetByIdSaveViewModel(int id);
        Task Update(DoctorViewModel doctorToSave);
        Task Add(DoctorViewModel doctorToCreate);
        Task Delete(int id);
    }
}

