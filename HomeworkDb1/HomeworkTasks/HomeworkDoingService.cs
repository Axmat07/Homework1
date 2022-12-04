using Bogus;
using Castle.Core.Internal;
using HomeworkDb1.Abstractions;
using HomeworkDb1.Domain.Entity;

namespace HomeworkDb1.HomeworkTasks;

public class HomeworkDoingService : IHomeworkDoingService
{
    private readonly Faker _faker = new ();
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<Lector> _lectorRepository;
    public  HomeworkDoingService(IRepository<Student> studentRepository, IRepository<Course> courseRepository, IRepository<Lector> lectorRepository)
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

    public async Task PrintTables()
    {
        await PrintCourses();
        await PrintStudents();
        await PrintLectors();
    }

    public async Task AddToTable()
    {
        while (true)
        {
            Console.WriteLine("Хотите добавить в таблицу?Y/N");
            var answer = GetAnswer();
            if (!answer)
            {
                return;
            }

            Console.WriteLine("В какую таблицу хотите добавить информацию?\n1-Студенты\n2-Лекторы\n3-Курсы");
            var answerTable = GetAnswerTable();
            switch (answerTable)
            {
                case 1:
                    await AddToStudentTable();
                    break;
                case 2:
                    await AddToLectorTable();
                    break;
                case 3:
                    await AddToCourseTable();
                    break;
            }

            Console.WriteLine("Хотите добавить в ещё одну таблицу?Y/N");
            var answerRepeat = GetAnswer();
            if (answerRepeat)
            {
                continue;
            }

            break;
        }
    }

    private async Task AddToCourseTable()
    {
        var course = new Course()
        {
            Id = Guid.NewGuid(),
            Name = GetStringField("Введите название курса:"),
            StartDate = DateTime.Today.ToUniversalTime()
        };
        await _courseRepository.AddAsync(course);
    }

    private async Task AddToLectorTable()
    {
        var lector = new Lector()
        {
            Id = Guid.NewGuid(),
            FirstName = GetStringField("Введите имя:"),
            LastName = GetStringField("Введите фамилию:"),
            Email = GetStringField("Введите email:"),
            Courses = await ChooseCourses(),
            Salary = GetIntWithMaximum(350000)
        };
        await _lectorRepository.AddAsync(lector);
    }

    private async Task AddToStudentTable()
    {
        var student = new Student()
        {
            Id = Guid.NewGuid(),
            FirstName = GetStringField("Введите имя:"),
            LastName = GetStringField("Введите фамилию:"),
            Email = GetStringField("Введите email:"),
            Courses = await ChooseCourses()
        };
        await _studentRepository.AddAsync(student);
    }

    private async Task<List<Course>> ChooseCourses()
    {
        var chosenCourses = new List<Course>();
        var allCourses = (await _courseRepository.GetAllAsync()).ToList();

        while (true)
        {
            Console.WriteLine("Выберите курс для студента:");
            for (int i = 0; i < allCourses.Count; i++)
            {
                Console.WriteLine($"{i+1} - {allCourses[i].Name}");
            }

            var numberOfChosenCourse = GetIntWithMaximum(allCourses.Count);
            chosenCourses.Add(allCourses[numberOfChosenCourse-1]);
            Console.WriteLine("Хотите выбрать ещё курс для студента");
            var answer = GetAnswer();
            if (!answer)
            {
                break;
            }
        }

        return chosenCourses;
    }

    private int GetIntWithMaximum(int count)
    {
        while (true)
        {
            Console.WriteLine("Выберите порядковый номер курса");
            var answer = Console.ReadLine()?.ToLower();
            var isItInt = int.TryParse(answer, out var answerInt);
            if (!isItInt || answerInt < 1 || answerInt > count)
            {
                continue;
            }

            return answerInt;
        }
    }

    private string GetStringField(string text)
    {
        while (true)
        {
            Console.WriteLine(text);
            var str = Console.ReadLine();
            var isNullOrEmpty = str.IsNullOrEmpty();
            if (isNullOrEmpty)
            {
                continue;
            }

            return str!;
        }
    }

    private int GetAnswerTable()
    { 
        while (true)
        {
            Console.WriteLine("Введите число от 1 до 3");
            var answer = Console.ReadLine()?.ToLower();
            var isItInt = int.TryParse(answer, out var answerInt);
            if (!isItInt)
            {
                continue;
            }
            switch (answerInt)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
            }
        }
    }

    private bool GetAnswer()
    {
        while (true)
        {
            var answer = Console.ReadLine()?.ToLower();
            switch (answer)
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    Console.WriteLine("Вы ввели неверные данные, если да введите \"Y\", если нет введите \"N\"");
                    break;
            }
        }
    }

    private async Task PrintLectors()
    {
        var allLectors = (await _lectorRepository.GetAllAsync()).ToList();
        if (!allLectors.Any())
        {
            Console.WriteLine("Лекторов нет");
            Console.WriteLine();
        }

        Console.WriteLine("Лекторы:");
        foreach (var lector in allLectors)
        {
            Console.WriteLine($"Name: {lector.FirstName} {lector.LastName}, Email: {lector.Email}, Salary: {lector.Salary}");
        }
    }

    private async Task PrintStudents()
    {
        var allStudents = (await _studentRepository.GetAllAsync()).ToList();
        if (!allStudents.Any())
        {
            Console.WriteLine("Студентов нет");
            Console.WriteLine();
        }

        Console.WriteLine("Студенты:");
        foreach (var student in allStudents)
        {
            Console.WriteLine($"Name: {student.FirstName} {student.LastName}, Email: {student.Email}");
        }
    }

    private async Task PrintCourses()
    {
        var allCourse = (await _courseRepository.GetAllAsync()).ToList();
        if (!allCourse.Any())
        {
            Console.WriteLine("Курсов нет");
            Console.WriteLine();
        }

        Console.WriteLine("Курсы:");
        foreach (var course in allCourse)
        {
            Console.WriteLine(course.Name);
        }
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
            var course = new Course
            {
                Name = _faker.Company.Bs(),
                Id = Guid.NewGuid(),
                Lectors = await GetRandomLector(),
                StartDate = _faker.Date.Past().ToUniversalTime(),
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