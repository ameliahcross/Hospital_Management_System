using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.LabResult;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class LabResultService : ILabResultService
    {
		private readonly ILabResultRepository _repository;

		public LabResultService(ILabResultRepository repository)
		{
			_repository = repository;
		}

        public async Task<List<LabResultViewModel>> GetAllViewModel()
        {
            var labResultsList = await _repository.GetAllAsync();

            return labResultsList.Select(labResult => new LabResultViewModel
            {
                Id = labResult.Id,
                //ResultName = labResult.Result,
                //PatientId = labResult.PatientId,
                PatientName = labResult.Patient.FirstName + " " + labResult.Patient.LastName,
                PatientIdentificationNumber = labResult.Patient.IdentificationNumber,
                //LabTestId = labResult.LabTestId,
                LabTestName = labResult.LabTest.Name,
                Status = labResult.Status
            }).ToList();
        }

        public async Task<SaveLabResultViewModel> GetByIdSaveViewModel(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);
            SaveLabResultViewModel labResultViewModel = new();
            labResultViewModel.Id = labResult.Id;
            //labResultViewModel.Name = labResult.Result;
            //labResultViewModel.PatientId = labResult.PatientId;
            //labResultViewModel.LabTestId = labResult.LabTestId;
            labResultViewModel.Status = labResult.Status;
            return labResultViewModel;
        }

        public async Task Update(SaveLabResultViewModel labResultToSave)
        {
            LabResult labResult = new();
            labResult.Id = labResultToSave.Id;
            labResult.Status = labResultToSave.Status;
            await _repository.UpdateAsync(labResult);
        }

        public async Task Add(SaveLabResultViewModel labResultToCreate)
        {
            LabResult labResult = new();
            labResult.Id = labResultToCreate.Id;
            //labResult.Result = labResultToCreate.Name;
            labResult.Status = labResultToCreate.Status;

            await _repository.AddAsync(labResult);
        }

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

