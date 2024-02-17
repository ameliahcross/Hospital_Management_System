using System;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.LabResult
{
	public class LabResultViewModel
	{
        public int Id { get; set; }
        public string ResultName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientIdentificationNumber { get; set; }


        public int LabTestId {get; set; }
        public string LabTestName { get; set; }
        public LabResultStatus Status { get; set; }
     
        public LabResultViewModel()
		{
		}
	}
}

