using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class LabTestService : ILabTestService
	{
        private readonly ILabTestRepository _repository;
        private readonly IAppointmentRepository _repositoryAppointment;

        public LabTestService(ILabTestRepository repository, IAppointmentRepository repositoryAppointment)
		{
            _repository = repository;
            _repositoryAppointment = repositoryAppointment;
        }

        public async Task<List<LabTestViewModel>> GetAllViewModel()
        {
            var labTestsList = await _repository.GetAllAsync();
            return labTestsList.Select(labTest => new LabTestViewModel
            {
                Id = labTest.Id,
                Name = labTest.Name
            }).ToList();
        }

        public async Task<SaveLabTestViewModel> GetByIdSaveViewModel(int id)
        {
            var labTest = await _repository.GetByIdAsync(id);
            SaveLabTestViewModel labTestViewModel = new();
            labTestViewModel.Id = labTest.Id;
            labTestViewModel.Name = labTest.Name;
            return labTestViewModel;
        }

        public async Task Update(SaveLabTestViewModel labTestToSave)
        {
            LabTest labTest = new();
            labTest.Id = labTestToSave.Id;
            labTest.Name = labTestToSave.Name;
            await _repository.UpdateAsync(labTest);
        }

        public async Task Add(SaveLabTestViewModel labTestToCreate)
        {
            LabTest labTest = new();
            labTest.Id = labTestToCreate.Id;
            labTest.Name = labTestToCreate.Name;
            await _repository.AddAsync(labTest);
        }

        public async Task Delete(int id)
        {
            var labTest = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(labTest);
        }

        // adicional
        public async Task<List<LabTestViewModel>> GetAvailableLabTestsAsync()
        {
            var labTests = await _repository.GetAllAsync();
            var labTestViewModels = labTests.Select(lt => new LabTestViewModel
            {
                Id = lt.Id,
                Name = lt.Name
            }).ToList();
            return labTestViewModels;
        }


    }
}

