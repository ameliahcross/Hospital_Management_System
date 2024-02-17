using System;
using HospitalApp.Core.Application.Interfaces.Repositories;
using HospitalApp.Core.Application.Interfaces.Services;
using HospitalApp.Core.Application.ViewModels.Doctor;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.Services
{
	public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
		{
            _repository = repository;
        }

        public async Task<List<DoctorViewModel>> GetAllViewModel()
        {
            var doctorsList = await _repository.GetAllAsync();

            return doctorsList.Select(doctor => new DoctorViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                IdentificationNumber = doctor.IdentificationNumber,
                Photo = doctor.Photo
            }).ToList();
        }

        public async Task<DoctorViewModel> GetByIdSaveViewModel(int id)
        {
            var doctor = await _repository.GetByIdAsync(id);
            DoctorViewModel doctorViewModel = new();
            doctorViewModel.FirstName = doctor.FirstName;
            doctorViewModel.LastName = doctor.LastName;
            doctorViewModel.Email = doctor.Email;
            doctorViewModel.PhoneNumber = doctor.PhoneNumber;
            doctorViewModel.IdentificationNumber = doctor.IdentificationNumber;
            doctorViewModel.Photo = doctor.Photo;

            return doctorViewModel;
        }

        public async Task Update(DoctorViewModel doctorToSave)
        {
            Doctor doctor = new();
            doctor.Id = doctorToSave.Id;
            doctor.FirstName = doctorToSave.FirstName;
            doctor.LastName = doctorToSave.LastName;
            doctor.Email = doctorToSave.Email;
            doctor.PhoneNumber = doctorToSave.PhoneNumber;
            doctor.IdentificationNumber = doctorToSave.IdentificationNumber;
            doctor.Photo = doctorToSave.Photo;

            await _repository.UpdateAsync(doctor);
        }

        public async Task Add(DoctorViewModel doctorToCreate)
        {
            Doctor doctor = new();
            doctor.Id = doctorToCreate.Id;
            doctor.FirstName = doctorToCreate.FirstName;
            doctor.LastName = doctorToCreate.LastName;
            doctor.Email = doctorToCreate.Email;
            doctor.PhoneNumber = doctorToCreate.PhoneNumber;
            doctor.IdentificationNumber = doctorToCreate.IdentificationNumber;
            doctor.Photo = doctorToCreate.Photo;

            await _repository.AddAsync(doctor);
        }

        public async Task Delete(int id)
        {
            var doctor = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(doctor);
        }
    }
}

