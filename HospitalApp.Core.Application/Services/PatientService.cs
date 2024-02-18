using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Patient;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class PatientService : IPatientSevice
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
		{
            _repository = repository;
        }

        public async Task<List<PatientViewModel>> GetAllViewModel()
        {
            var patientsList = await _repository.GetAllAsync();
            return patientsList.Select(patient => new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                IdentificationNumber = patient.IdentificationNumber,
                Address = patient.Address,
                DateOfBirth = patient.DateOfBirth,
                IsSmoker = patient.IsSmoker,
                HasAllergies = patient.HasAllergies,
                Photo = patient.Photo
            }).ToList();
        }

        public async Task<SavePatientViewModel> GetByIdSaveViewModel(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            SavePatientViewModel patientViewModel = new();
            patientViewModel.Id = patient.Id;
            patientViewModel.FirstName = patient.FirstName;
            patientViewModel.LastName = patient.LastName;
            patientViewModel.DateOfBirth = patient.DateOfBirth;
            patientViewModel.PhoneNumber = patient.PhoneNumber;
            patientViewModel.Address = patient.Address;
            patientViewModel.HasAllergies = patient.HasAllergies;
            patientViewModel.IsSmoker = patient.IsSmoker;
            patientViewModel.IdentificationNumber = patient.IdentificationNumber;
            patientViewModel.Photo = patient.Photo;

            return patientViewModel;
        }

        public async Task Update(SavePatientViewModel patientToSave)
        {
            Patient patient = new();
            patient.Id = patientToSave.Id;
            patient.FirstName = patientToSave.FirstName;
            patient.LastName = patientToSave.LastName;
            patient.HasAllergies = patientToSave.HasAllergies;
            patient.Address = patientToSave.Address;
            patient.DateOfBirth = patientToSave.DateOfBirth;
            patient.PhoneNumber = patientToSave.PhoneNumber;
            patient.IsSmoker = patientToSave.IsSmoker;
            patient.IdentificationNumber = patientToSave.IdentificationNumber;
            patient.Photo = patientToSave.Photo;

            await _repository.UpdateAsync(patient);
        }

        public async Task Add(SavePatientViewModel patientToCreate)
        {
            Patient patient = new();
            patient.Id = patientToCreate.Id;
            patient.FirstName = patientToCreate.FirstName;
            patient.LastName = patientToCreate.LastName;
            patient.HasAllergies = patientToCreate.HasAllergies;
            patient.Address = patientToCreate.Address;
            patient.DateOfBirth = patientToCreate.DateOfBirth;
            patient.PhoneNumber = patientToCreate.PhoneNumber;
            patient.IsSmoker = patientToCreate.IsSmoker;
            patient.IdentificationNumber = patientToCreate.IdentificationNumber;
            patient.Photo = patientToCreate.Photo;

            await _repository.AddAsync(patient);
        }

        public async Task Delete(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(patient);
        }

    }
}

