using System;

namespace Clinicamedica.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public int PacienteId { get; set; }
        public int TratamientoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        // Relación con Paciente y Tratamiento
        public Paciente? Paciente { get; set; }
        public Tratamiento? Tratamiento { get; set; }
    }
}
