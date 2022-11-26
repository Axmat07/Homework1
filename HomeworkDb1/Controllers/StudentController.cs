using HomeworkDb1.Abstractions;
using HomeworkDb1.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkDb1.Controllers;


[Route("api/[controller]")]
[ApiController]

public class StudentController : ControllerBase
{
    private readonly IRepository<Student> _studentRepository;

    public StudentController(IRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    [HttpGet]
    public async Task<IEnumerable<User>> Get()
    {
        var students = await _studentRepository.GetAllAsync();
        return students;
    }
    
    [HttpGet("{id}")]
    public async Task<User?> Get(Guid id)
    {
        var student = await _studentRepository.GetAsync(id);
        return student;
    }
    
    [HttpPost]
    public async Task<Student> Post([FromBody] Student entity)
    {
        entity.Id = Guid.NewGuid();
        await _studentRepository.AddAsync(entity);
        return entity;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Student entity)
    {
        var student = await _studentRepository.GetAsync(id);
        if (student == null)
        {
            return BadRequest("Пользователь не существует");
        }

        await _studentRepository.UpdateAsync(entity);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _studentRepository.GetAsync(id);
        if (student == null)
        {
            return BadRequest("Такого студента не существует");
        }

        await _studentRepository.DeleteAsync(id);
        return Ok();
    }
}