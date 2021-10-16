public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime LastModificationAt { get; set; }
    public string? CreateBy { get; set; }
    public string? UpdateBy { get; set; }
    public string? DeleteBy { get; set; }
    public bool IsDelete { get; set; }
}