using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class LabResultService: ILabResultService
    {
		private readonly ILabResultRepository _repository;

		public LabResultService(ILabResultRepository repository)
		{
			_repository = repository;
		}

        public async Task<List<SaveLabResultViewModel>> GetAllViewModel()
        {
            var labResultsList = await _repository.GetAllAsyncWithInclude();

            var saveLabResultViewModels = labResultsList.Select(labResult => new SaveLabResultViewModel
            {
                LabTestName = labResult.LabTest.Name,
                Status = labResult.Status,
                PatientName = labResult.Appointment.Patient.FirstName + " " + labResult.Appointment.Patient.LastName,
                AppointmentId = labResult.AppointmentId
            }).ToList();

            return saveLabResultViewModels;
        }

        //public async Task<SaveLabResultViewModel> GetByIdSaveViewModel(int id)
        //{
        //    var labResult = await _repository.GetByIdAsync(id);
        //    SaveLabResultViewModel labResultViewModel = new();
        //    labResultViewModel.Id = labResult.Id;
        //    labResultViewModel.Status = labResult.Status;
        //    return labResultViewModel;
        //}

        //public async Task Update(SaveLabResultViewModel labResultToSave)
        //{
        //    LabResult labResult = new();
        //    labResult.Id = labResultToSave.Id;
        //    labResult.Status = labResultToSave.Status;
        //    await _repository.UpdateAsync(labResult);
        //}

        //public async Task Add(SaveLabResultViewModel labResultToCreate)
        //{
        //    LabResult labResult = new();
        //    labResult.Id = labResultToCreate.Id;
        //    labResult.Status = labResultToCreate.Status;

        //    await _repository.AddAsync(labResult);
        //}

        public async Task Delete(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(labResult);
        }

        public async Task CreateLabResultsAsync(List<SaveLabResultViewModel> labResultSaveViewModels)
        {
            foreach (var labResultSaveViewModel in labResultSaveViewModels)
            {
                var labResult = new LabResult
                {
                    Result = "",
                    Status = LabResultStatus.Pendiente,
                    LabTestId = labResultSaveViewModel.LabTestId,
                    AppointmentId = labResultSaveViewModel.AppointmentId
                };
                await _repository.AddAsync(labResult);
            }
        }
    }
}

