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

        public async Task<List<LabResultViewModel>> GetAllViewModel()
        {
            var labResultsList = await _repository.GetAllAsyncWithInclude();

            var labResultViewModels = labResultsList.Select(labResult => new LabResultViewModel
            {
                LabTestName = labResult.LabTest.Name,
                Status = labResult.Status,
                PatientName = labResult.Appointment.Patient.FirstName + " " + labResult.Appointment.Patient.LastName,
                AppointmentId = labResult.AppointmentId
            }).ToList();

            return labResultViewModels;
        }

        public async Task<List<SaveLabResultViewModel>> GetLabResultsByAppointmentId(int appointmentId)
        {
            var labResults = await _repository.GetLabResultByAppointmentIdAsync(appointmentId);

            var labResultViewModels = labResults.Select(result => new SaveLabResultViewModel
            {
                Id = result.Id,
                Status = result.Status,
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
                // Obtener el LabTest asociado al LabResult
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





        public async Task Update(SaveLabResultViewModel labResultToSave)
        {
            LabResult labResult = new();
            //labResult.Id = labResultToSave.Id;
            labResult.Status = labResultToSave.Status;
            await _repository.UpdateAsync(labResult);
        }

        public async Task Add(SaveLabResultViewModel labResultToCreate)
        {
            LabResult labResult = new();
            //labResult.Id = labResultToCreate.Id;
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

