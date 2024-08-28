using System;
using System.Collections.Generic;

namespace Clinicamedica.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Relación con Cita
        public ICollection<Cita> Citas { get; set; }
    }
}