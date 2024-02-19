using System;
namespace HospitalApp.Core.Application.ViewModels.LabTest
{
	public class SelectLabTestsViewModel
	{
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public List<LabTestSelection> AvailableLabTests { get; set; }
        public List<LabTestSelection> SelectedLabTests { get; set; }

        public class LabTestSelection
        {
            public int LabTestId { get; set; }
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}

