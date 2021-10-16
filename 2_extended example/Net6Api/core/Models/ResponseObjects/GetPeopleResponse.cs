namespace net6.core.Models
{
    public class GetPeopleResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public uint Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Telefono { get; set; }
    }
}
