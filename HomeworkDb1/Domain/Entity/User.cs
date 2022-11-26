using HomeworkDb1.Abstractions;

namespace HomeworkDb1.Domain.Entity;

public abstract class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}