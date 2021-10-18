using System;

namespace simple.shared.Models
{
    public class DeveloperDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Edad { get; set; }
        public decimal Sueldo { get; set; }
        public int Experiencia { get; set; }
    }
}
