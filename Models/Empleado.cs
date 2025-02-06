using System.ComponentModel.DataAnnotations;

namespace Practica1.Models
{
    public class Empleado
    {
        [Key]
        public int EmpleadoId { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Contrasena { get; set; }

    }
}
