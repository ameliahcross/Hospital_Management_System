using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class LabTestService : ILabTestService
	{
        private readonly ILabTestRepository _repository;

        public LabTestService(ILabTestRepository repository)
		{
            _repository = repository;
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

        public async Task<LabTestViewModel> GetByIdSaveViewModel(int id)
        {
            var labTest = await _repository.GetByIdAsync(id);
            LabTestViewModel labTestViewModel = new();
            labTestViewModel.Id = labTest.Id;
            labTestViewModel.Name = labTest.Name;
            return labTestViewModel;
        }

        public async Task Update(LabTestViewModel labTestToSave)
        {
            LabTest labTest = new();
            labTest.Id = labTestToSave.Id;
            labTest.Name = labTestToSave.Name;
            await _repository.UpdateAsync(labTest);
        }

        public async Task Add(LabTestViewModel labTestToCreate)
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
    }
}

