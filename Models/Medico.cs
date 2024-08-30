using System.Collections.Generic;

namespace Clinicamedica.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }
        public string? Nombre { get; set; }
        public string? Especialidad { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        // Relación con Cita
        public ICollection<Cita>? Citas { get; set; }
    }
}
