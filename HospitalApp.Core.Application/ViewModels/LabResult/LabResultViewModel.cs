﻿using System;
using HospitalApp.Core.Application.ViewModels.LabTest;
using HospitalApp.Core.Application.ViewModels.Patient;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.LabResult
{
	public class LabResultViewModel
	{
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Cedula { get; set; }
        public string ResultadoDigitado { get; set; }
        public string PatientIdentificationNumber { get; set; }
        public string LabTestName { get; set; }
        public LabResultStatus Status { get; set; }
        public int AppointmentId { get; set; }

        public IEnumerable<PatientViewModel> Patients { get; set; }
        public IEnumerable<LabTestViewModel> LabTests { get; set; }
        public List<int> LabResultIds { get; set; }

        public LabResultViewModel()
		{
            Patients = new List<PatientViewModel>();
            LabTests = new List<LabTestViewModel>();
            LabResultIds = new List<int>();
        }
	}
}

