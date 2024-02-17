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
                ResultName = labResult.Result,
                PatientId = labResult.PatientId,
                PatientName = labResult.Patient.FirstName + " " + labResult.Patient.LastName,
                PatientIdentificationNumber = labResult.Patient.IdentificationNumber,
                LabTestId = labResult.LabTestId,
                LabTestName = labResult.LabTest.Name,
                Status = labResult.Status
            }).ToList();
        }

        public async Task<LabResultViewModel> GetByIdSaveViewModel(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);
            LabResultViewModel labResultViewModel = new();
            labResultViewModel.ResultName = labResult.Result;
            labResultViewModel.PatientId = labResult.PatientId;
            labResultViewModel.PatientName = labResult.Patient.FirstName + " " + labResult.Patient.LastName;
            labResultViewModel.PatientIdentificationNumber = labResult.Patient.IdentificationNumber;
            labResultViewModel.LabTestId = labResult.LabTestId;
            labResultViewModel.LabTestName = labResult.LabTest.Name;
            labResultViewModel.Status = labResult.Status;
            return labResultViewModel;
        }

        public async Task Update(LabResultViewModel labResultToSave)
        {
            LabResult labResult = new();
            labResult.Id = labResultToSave.Id;
            labResult.Result = labResultToSave.ResultName;
            labResult.Status = labResultToSave.Status;
            await _repository.UpdateAsync(labResult);
        }

        public async Task Add(LabResultViewModel labResultToCreate)
        {
            LabResult labResult = new();
            labResult.Id = labResultToCreate.Id;
            labResult.Result = labResultToCreate.ResultName;
            labResult.Status = labResultToCreate.Status;

            await _repository.AddAsync(labResult);
        }

        public async Task Delete(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(labResult);
        }
    }
}

