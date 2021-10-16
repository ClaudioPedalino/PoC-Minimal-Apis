public class People : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    public string AvatarUrl { get; set; }
    public string Phone { get; set; }
}