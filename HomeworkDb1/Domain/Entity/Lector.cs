using HomeworkDb1.Abstractions;

namespace HomeworkDb1.Domain.Entity;

public class Lector : User
{
    public int Salary { get; set; }
    public List<Course> Courses { get; set; }
}