using AnimatLabs.Api.Contracts;
using AnimatLabs.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimatLabs.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase, ICrudOperations<Student>
{
    private static List<Student> _students = new List<Student>();

    [HttpGet]
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return _students.AsEnumerable();
    }

    [HttpGet("{id}")]
    public async Task<Student?> GetAsync(Guid id)
    {
        return _students.FirstOrDefault(student => student.Id == id);
    }

    [HttpPost]
    public async Task<Guid?> CreateAsync([FromBody] Student student)
    {
        if (student != null)
            _students.Add(student);

        return student?.Id;
    }

    [HttpPost("{id}")]
    public async Task<bool> UpdateAsync(Guid id, [FromBody] Student student)
    {
        var index = _students.FindIndex(s => s.Id == id);

        if (index >= 0)
        {
            _students[index] = student;
            return true;
        }

        return false;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(Guid id)
    {
        return _students.Remove(_students.FirstOrDefault(s => s.Id == id));
    }
}