namespace net6.core.Models
{
    public class GetBookResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public uint TotalPages { get; set; }
    }
}
