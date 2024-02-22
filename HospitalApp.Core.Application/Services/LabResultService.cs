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
        private readonly IAppointmentRepository _repositoryAppointment;
        private readonly ILabTestRepository _repositoryTest;


        public LabResultService(ILabResultRepository repository, IAppointmentRepository appointmentRepository, ILabTestRepository repositoryTest)
		{
			_repository = repository;
            _repositoryAppointment = appointmentRepository;
            _repositoryTest = repositoryTest;
        }

        public async Task<List<SaveLabResultViewModel>> GetLabResultsByAppointmentId(int appointmentId)
        {
            var labResults = await _repository.GetLabResultByAppointmentIdAsync(appointmentId);

            var labResultViewModels = labResults.Select(result => new SaveLabResultViewModel
            {
                AppointmentId = result.AppointmentId,
                PatientName = result.Appointment.Patient.FirstName + " " + result.Appointment.Patient.LastName,
                LabTestName = result.LabTest.Name,
                Status = result.Status
            }).ToList();
            return labResultViewModels;
        }

        public async Task<int?> GetLabResultIdForAppointment(int appointmentId)
        {
            var labResults = await _repository.GetLabResultByAppointmentIdAsync(appointmentId);
            if (labResults.Any())
            {
                return labResults.First().Id;
            }

            return null;
        }

        public async Task<SaveLabResultViewModel> GetByIdSaveViewModel(int id)
        {
            var labResult = await _repository.GetByIdAsync(id);

            if (labResult != null)
            {
                var labTest = await _repositoryTest.GetByIdAsync(labResult.LabTestId);

                var appointment = await _repositoryAppointment.GetByIdAsyncWithRelations(labResult.AppointmentId);

                SaveLabResultViewModel labResultViewModel = new();
                labResultViewModel.Id = labResult.Id;
                labResultViewModel.Status = labResult.Status;
                labResultViewModel.AppointmentId = labResult.AppointmentId;
                labResultViewModel.LabTestName = labTest.Name;
                labResultViewModel.PatientName = appointment.Patient.FirstName + " " + appointment.Patient.LastName;

                return labResultViewModel;
            }
            else
            {
                throw new Exception($"No se encontró ningún resultado de laboratorio con el Id {id}");
            }
        }

        public async Task<List<LabResultViewModel>> GetCompletedAsync(int appointmentId)
        {
            var labResults = await _repository.GetCompletedAsync();

            var labResultsViewModel = labResults
                .Where(lr => lr.AppointmentId == appointmentId)
                .Select(lr => new LabResultViewModel
                {
                    ResultadoDigitado = lr.Result,
                    LabTestName = lr.LabTest?.Name,
                    PatientName = lr.Appointment.Patient.LastName,
                    AppointmentId = lr.AppointmentId,
                    Status = lr.Status
                }).ToList();

            return labResultsViewModel;
        }


        public async Task Update(SaveLabResultViewModel labResultToSave)
        {
            var labResult = await _repository.GetByIdAsync(labResultToSave.Id);

            if (labResult == null)
            {
                throw new Exception($"No se pudo encontrar el resultado del laboratorio con el ID: {labResultToSave.Id}.");
            }

            labResult.Result = labResultToSave.Result;
            await _repository.UpdateAsync(labResult);
        }

        public async Task Add(SaveLabResultViewModel labResultToCreate)
        {
            LabResult labResult = new();
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

        public async Task ChangeLabResultStatusAsync(int Id, LabResultStatus newStatus)
        {
            var labResult = await _repository.GetByIdAsync(Id);

            if (labResult != null)
            {
                labResult.Status = newStatus;
                await _repository.UpdateAsync(labResult);
            }
        }

        public async Task<List<LabResultViewModel>> GetAllViewModel()
        {
            var labResultsList = await _repository.GetAllAsyncWithInclude();

            var labResultViewModels = labResultsList.Select(labResult => new LabResultViewModel
            {
                Id = labResult.Id,
                Cedula = labResult.Appointment.Patient.IdentificationNumber,
                LabTestName = labResult.LabTest.Name,
                Status = labResult.Status,
                PatientName = labResult.Appointment.Patient.FirstName + " " + labResult.Appointment.Patient.LastName,
                AppointmentId = labResult.AppointmentId
            }).ToList();

            return labResultViewModels;
        }


        public async Task<List<LabResultViewModel>> GetAllViewModelFiltered(string cedula)
        {
            var labResults = await _repository.GetAllFilteredAsync(cedula);

            var labResultsViewModel = labResults
                                     .Where(result => result.Appointment != null && result.Appointment.Patient != null) 
                                     .Select(result => new LabResultViewModel
                                     {
                                         Id = result.Id,
                                         Cedula = result.Appointment.Patient.IdentificationNumber,
                                         LabTestName = result.LabTest.Name,
                                         Status = result.Status,
                                         PatientName = result.Appointment.Patient.FirstName + " " + result.Appointment.Patient.LastName,
                                         AppointmentId = result.AppointmentId
                                     })
                                     .ToList();


            return labResultsViewModel;
        }

    }
}

