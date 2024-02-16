using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Application.ViewModels.Patient;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.LabResult
{
	public class SaveLabResultViewModel
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar el resultado")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un paciente")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una prueba")]
        public int LabTestId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estatus")]
        public LabResultStatus Status { get; set; }

        public IEnumerable<PatientViewModel> Patients { get; set; }
        public IEnumerable<LabTestViewModel> LabTests { get; set; }

        public SaveLabResultViewModel()
		{
            Patients = new List<PatientViewModel>();
            LabTests = new List<LabTestViewModel>();
        }
	}
}

