using System;
namespace HospitalApp.Core.Application.ViewModels.Appointment
{
	public class DeleteAppointmentViewModel
	{
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
    }
}

