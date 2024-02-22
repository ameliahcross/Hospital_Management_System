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

        [Required(ErrorMessage = "Debe colocar un resultado")]
        public string Result { get; set; }

        public LabResultStatus Status { get; set; }

        public string LabTestName { get; set; }
        public string PatientName { get; set; }


        public int LabTestId { get; set; }
        public int AppointmentId { get; set; }

        public IEnumerable<PatientViewModel> Patients { get; set; }
        public IEnumerable<LabTestViewModel> LabTests { get; set; }

        public SaveLabResultViewModel()
		{
            Patients = new List<PatientViewModel>();
            LabTests = new List<LabTestViewModel>();
        }
	}
}

