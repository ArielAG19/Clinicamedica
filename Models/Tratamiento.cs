namespace Clinicamedica.Models
{
    public class Tratamiento
    {
        public int TratamientoId { get; set; }
        public string Descripcion { get; set; }
        public int CitaId { get; set; }

        // Relación con Cita
        public Cita Cita { get; set; }
    }
}
