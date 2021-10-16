namespace net6.core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public decimal Precio { get; set; }
        public uint CantidadPaginas { get; set; }
    }
}
