public class Event
{
    [Key]
    public string Trace { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public string Producer { get; set; }
}
