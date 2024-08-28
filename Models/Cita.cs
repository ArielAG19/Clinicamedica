using System;

namespace Clinicamedica.Models
{
    public class Cita
    {
        public int CitaId { get; set; }
        public DateTime Fecha { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public string Motivo { get; set; }

        // Propiedades de navegación
        public Medico Medico { get; set; } = default!;
        public Paciente Paciente { get; set; } = default!;
    }
}
