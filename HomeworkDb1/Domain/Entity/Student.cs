using HomeworkDb1.Abstractions;

namespace HomeworkDb1.Domain.Entity;

public class Student : User
{
    public List<Course>? Courses { get; set; }
}