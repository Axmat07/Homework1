using Bogus;
using HomeworkDb1.Abstractions;
using HomeworkDb1.Domain.Entity;

namespace HomeworkDb1;

public class BuildTable : IBuildTable
{
    private readonly Faker _faker = new ();
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Lector> _lectorRepository;
    public  BuildTable(IRepository<Student> studentRepository, IRepository<Course> courseRepository, IRepository<Lector> lectorRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _lectorRepository = lectorRepository;
    }
    
    public async Task FillTable()
    {
        await FillStudents();
        await FillLectors();
        await FillCourses();
    }

    private async Task FillLectors()
    {
        var lectors =  GenerateLectors(5);
        foreach (var lector in lectors)
        {
            await _lectorRepository.AddAsync(lector);
        }
    }

    private async Task FillCourses()
    {
        var courses = await GenerateCourses(5);
        foreach (var course in courses)
        {
            await _courseRepository.AddAsync(course);
        }
    }

    private async Task<List<Course>> GenerateCourses(int count)
    {
        var courses = new List<Course>();
        for (int i = 0; i < count; i++)
        {
            var course = new Course()
            {
                Id = Guid.NewGuid(),
                Lectors = await GetRandomLector(),
                StartDate = _faker.Date.Past(),
                Students = await GetRandomStudents()
            };
            courses.Add(course);
        }
        return courses;
    }

    private async Task<List<Student>> GetRandomStudents()
    {
        var students = (await _studentRepository.GetAllAsync()).ToList();
        var countOfStudents = _faker.Random.Int(1, students.Count);
        var studentsToReturn = new List<Student>();
        for (int i = 0; i < countOfStudents; i++)
        {
            var randomStudent = _faker.Random.Int(0, students.Count-1);
            studentsToReturn.Add(students[randomStudent]);
            students.RemoveAt(randomStudent);
        }

        return studentsToReturn;
    }

    private async Task<List<Lector>> GetRandomLector()
    {
        var lectors = (await _lectorRepository.GetAllAsync()).ToList();
        var countOfLectors = _faker.Random.Int(1, lectors.Count);
        var lectorsToReturn = new List<Lector>();
        for (int i = 0; i < countOfLectors; i++)
        {
            var randomLector = _faker.Random.Int(0, lectors.Count-1);
            lectorsToReturn.Add(lectors[randomLector]);
            lectors.RemoveAt(randomLector);
        }

        return lectorsToReturn;
    }

    private async Task FillStudents()
    {
        var students = GenerateUsers(5);
        foreach (var student in students)
        {
            await _studentRepository.AddAsync(student);
        }
    }

    private List<Student> GenerateUsers(int count)
    {
        var students = new List<Student>();
        for (var i = 0; i < count; i++)
        {
            var student = new Student()
            {
                Courses = null,
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Id = Guid.NewGuid()
            };
            students.Add(student);
        }

        return students;
    }
    
    private IEnumerable<Lector> GenerateLectors(int count)
    {
        var lectors = new List<Lector>();
        for (int i = 0; i < count; i++)
        {
            var lector = new Lector()
            {
                Id = Guid.NewGuid(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Salary = _faker.Random.Int(50000, 350000)
            };
            lectors.Add(lector);
        }

        return lectors;
    }

}