﻿using Bogus;
using HomeworkDb1.Abstractions;
using HomeworkDb1.Domain.Entity;

namespace HomeworkDb1;

public class BuildTable : IBuildTable
{
    private readonly Faker _faker = new Faker();
    private readonly IRepository<Student> _studentRepository;
    public  BuildTable(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task FillTable()
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
}