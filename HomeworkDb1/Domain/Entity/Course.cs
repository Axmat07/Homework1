using HomeworkDb1.Abstractions;

namespace HomeworkDb1.Domain.Entity;

public class Course : BaseEntity
{
    public DateTime StartDate { get; set; }
    public List<Lector> Lectors { get; set; }
    public List<Student> Students { get; set; }
}