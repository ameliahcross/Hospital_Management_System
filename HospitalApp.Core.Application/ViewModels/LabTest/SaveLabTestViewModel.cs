using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Core.Application.ViewModels.LabTest
{
	public class SaveLabTestViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la prueba")]
        public string Name { get; set; }

        public SaveLabTestViewModel()
		{
		}
	}
}

