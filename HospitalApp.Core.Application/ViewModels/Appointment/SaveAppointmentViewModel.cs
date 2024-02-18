using System;
using System.ComponentModel.DataAnnotations;
using HospitalApp.Core.Application.ViewModels.Doctor;
using HospitalApp.Core.Application.ViewModels.Patient;
using HospitalApp.Core.Domain.Entities;

namespace HospitalApp.Core.Application.ViewModels.Appointment
{
	public class SaveAppointmentViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una hora")]
        [DataType(DataType.Time, ErrorMessage = "Debe ingresar una hora válida")]
        public TimeOnly Time { get; set; }

        [Required(ErrorMessage = "Debe colocar una razón para la cita")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un estatus")]
        public AppointmentStatus Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el doctor")]
        public int DoctorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar el paciente")]
        public int PatientId { get; set; }

        public IEnumerable<DoctorViewModel> Doctors { get; set; }
        public IEnumerable<PatientViewModel> Patients { get; set; }

        public SaveAppointmentViewModel()
		{
            Doctors = new List<DoctorViewModel>();
            Patients = new List<PatientViewModel>();
        }
	}
}

